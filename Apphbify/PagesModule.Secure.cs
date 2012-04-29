using System;
using System.Collections.Generic;
using AppHarbor;
using AppHarbor.Model;
using Apphbify.Services;
using Apphbify.ViewModels;
using Nancy;

namespace Apphbify
{
    public class SecuredPagesModule : NancyModule
    {
        private AppHarborApi _Api;
        private DataStore _Data;

        public SecuredPagesModule(DataStore data)
        {
            _Data = data;
            Before.AddItemToEndOfPipeline(CheckAuth);

            Get["/Sites"] = Sites;
            Put["/Sites/{slug}/Notifications/Email"] = EnableEmailNotification;
            Get["/Deploy/{key}"] = Deploy;
            Post["/Deploy/{key}"] = DoDeploy;
        }

        private Response Sites(dynamic parameters)
        {
            return View["Sites", new SitesViewModel(_Api, Request.Session)];
        }

        private Response EnableEmailNotification(dynamic parameters)
        {
            string email = Request.Form["email"];
            string slug = parameters.slug;
            if (String.IsNullOrEmpty(email))
                return Response.AsJson(JsonResult.Error("Please provide an email address."), HttpStatusCode.BadRequest);

            var result = _Api.CreateServicehook(slug, String.Format("http://appharbify.com/Sites/{0}/NotifyByEmail?email={1}", slug, Uri.EscapeDataString(email)));

            if (result.Status != CreateStatus.Created)
                return Response.AsJson(JsonResult.Error("Unable to add service hook."), HttpStatusCode.BadRequest);

            return Response.AsJson(JsonResult.OK());
        }

        private Response Deploy(dynamic parameters)
        {
            var app = _Data.GetAppByKey((string)parameters.key);
            if (app == null) return Response.AsRedirect("/Apps").WithErrorFlash(Session, String.Format("App {0} not found.", (string)parameters.key));

            return View["Deploy", new DeployViewModel(app, Request.Session)];
        }

        private Response DoDeploy(dynamic parameters)
        {
            var app = _Data.GetAppByKey((string)parameters.key);
            string appName = Request.Form.application_name;
            if (app == null) return Response.AsRedirect("/Apps").WithErrorFlash(Session, String.Format("App {0} not found.", (string)parameters.key));
            if (String.IsNullOrWhiteSpace(appName)) return Response.AsRedirect("/Deploy/" + app.Key).WithErrorFlash(Session, "Please enter an application name");

            string access_token = (string)Session[SessionKeys.ACCESS_TOKEN];
            string slug = "";

            // Build set of variables that need to be added to the application
            var variables = new Dictionary<string, string>();
            foreach (var variable in app.Variables)
            {
                string value = Request.Form[variable.Key];
                if (!String.IsNullOrEmpty(value))
                    variables.Add(variable.Key, value);
            }

            var result = DeploymentService.Deploy(_Api, access_token, appName, app, variables, out slug);

            // TODO: Log errors here, we want to know whether the API or the app config is at fault.
            switch (result)
            {
                case DeploymentResult.UnableToCreateApplication:
                    return Response.AsRedirect("/Deploy/" + app.Key).WithErrorFlash(Session, "There was a problem creating the application at AppHarbor.");
                case DeploymentResult.UnableToDeployCode:
                    return Response.AsRedirect("/Deploy/" + app.Key).WithErrorFlash(Session, "There was a problem deploying the application.");
                case DeploymentResult.ErrorInstallingAddons:
                    return Response.AsRedirect("/Sites").WithErrorFlash(Session, "Your site has been deployed but there were problems installing all required addons. The site may not operate as expected.");
                case DeploymentResult.ErrorSettingVariables:
                    return Response.AsRedirect("/Sites").WithErrorFlash(Session, "Your site has been deployed but there were problems setting all the configuration variables. The site may not operate as expected, please log in to AppHarbor to confirm.");
                case DeploymentResult.Success:
                    return Response.AsRedirect("/Sites").WithSuccessFlash(Session, String.Format("{0} deployed into site {1} ({2})", app.Name, appName, slug));
                default:
                    return Response.AsRedirect("/Deploy/" + app.Key).WithErrorFlash(Session, "There was a problem deploying the application. Double check your deployed application to see whether it was successful or not");
            }
        }

        private Response CheckAuth(NancyContext ctx)
        {
            var token = ctx.Request.Session[SessionKeys.ACCESS_TOKEN] as string;
            if (String.IsNullOrEmpty(token))
                return Response.AsRedirect("/SignIn?redirect=" + Uri.EscapeDataString(ctx.Request.Path));
            _Api = new AppHarborApi(new AuthInfo { AccessToken = token, TokenType = "Bearer" });
            return null;
        }
    }
}