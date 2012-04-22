using System;
using AppHarbor;
using Apphbify.ViewModels;
using Nancy;

namespace Apphbify
{
    public class SecuredPagesModule : NancyModule
    {
        private AppHarborApi _Api;

        public SecuredPagesModule()
        {
            Before.AddItemToEndOfPipeline(CheckAuth);

            Get["/Sites"] = Sites;
        }

        private Response Sites(dynamic parameters)
        {
            return View["Sites", new SitesViewModel(_Api, Request.Session)];
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