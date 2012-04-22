using System;
using Apphbify.Services;
using Apphbify.ViewModels;
using Nancy;

namespace Apphbify
{
    public class OAuthModule : NancyModule
    {
        private readonly OAuth _OAuth;

        public OAuthModule(OAuth api)
        {
            _OAuth = api;
            Get["/SignIn"] = SignIn;
            Get["/SignOut"] = SignOut;
            Get["/callback"] = Callback;
        }

        private Response SignIn(dynamic parameters)
        {
            return View["SignIn", new SignInViewModel(_OAuth, Request.Session)];
        }

        private Response SignOut(dynamic parameters)
        {
            Request.Session.DeleteAll();
            return Response.AsRedirect("/").WithSuccessFlash(Session, "Signed out!");
        }

        private Response Callback(dynamic parameters)
        {
            string access_token = "";
            if (Request.Query.code.HasValue) access_token = _OAuth.GetAccessToken(Request.Query.code);
            if (String.IsNullOrEmpty(access_token)) return Response.AsRedirect("/").WithErrorFlash(Session, "There was a problem signing you in. Please try again.");

            Request.Session[SessionKeys.ACCESS_TOKEN] = access_token;
            return Response.AsRedirect("/Apps").WithSuccessFlash(Session, "Signed in!");
        }
    }
}