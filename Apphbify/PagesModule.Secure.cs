using System;
using AppHarbor;
using AppHarbor.Model;
using Apphbify.Data;
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

            var result = _Api.CreateApplication(appName, "amazon-web-services::us-east-1");
            if (result.Status != CreateStatus.Created) return Response.AsRedirect("/Deploy/" + app.Key).WithErrorFlash(Session, "There was a problem creating the application at AppHarbor.");

            string access_token = (string)Session[SessionKeys.ACCESS_TOKEN];
            if (!BuildService.DeployBuild(access_token, result.ID, app.DownloadUrl)) return Response.AsRedirect("/Deploy/" + app.Key).WithErrorFlash(Session, "There was a problem deploying the application.");

            // Try to install all addons, even if one fails, and report on failure at the end.
            bool ok = true;
            foreach (var addon in app.Addons)
            {
                if (Addons.Supported.ContainsKey(addon))
                {
                    if (!BuildService.EnableAddon(access_token, result.ID, addon, Addons.Supported[addon]))
                        ok = false;
                }
                else
                {
                    ok = false;
                }
            }
            if (!ok) return Response.AsRedirect("/Sites").WithErrorFlash(Session, "Your site has been deployed but there were problems installing all required addons. The site may not operate as expected.");

            return Response.AsRedirect("/Sites").WithSuccessFlash(Session, String.Format("{0} deployed into site {1} ({2})", app.Name, appName, result.ID));
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