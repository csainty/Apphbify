using System;
using AppHarbor;
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
            Get["/Deploy/{key}"] = Deploy;
            Post["/Deploy/{key}"] = DoDeploy;
        }

        private Response Sites(dynamic parameters)
        {
            return View["Sites", new SitesViewModel(_Api, Request.Session)];
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

            var result = DeploymentService.Deploy(_Api, access_token, appName, app, out slug);

            // TODO: Log errors here, we want to know whether the API or the app config is at fault.
            switch (result)
            {
                case DeploymentResult.UnableToCreateApplication:
                    return Response.AsRedirect("/Deploy/" + app.Key).WithErrorFlash(Session, "There was a problem creating the application at AppHarbor.");
                case DeploymentResult.UnableToDeployCode:
                    return Response.AsRedirect("/Deploy/" + app.Key).WithErrorFlash(Session, "There was a problem deploying the application.");
                case DeploymentResult.ErrorInstallingAddons:
                    return Response.AsRedirect("/Sites").WithErrorFlash(Session, "Your site has been deployed but there were problems installing all required addons. The site may not operate as expected.");
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
                return Response.AsRedirect("/SignIn");
            _Api = new AppHarborApi(new AuthInfo { AccessToken = token, TokenType = "Bearer" });
            return null;
        }
    }
}