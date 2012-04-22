using System;
using Apphbify.Services;
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
            return Response.AsRedirect(_OAuth.GetAuthUrl());
        }

        private Response SignOut(dynamic parameters)
        {
            Request.Session.DeleteAll();
            Request.Session[SessionKeys.FLASH_SUCCESS] = "Signed out!";
            return Response.AsRedirect("/");
        }

        private Response Callback(dynamic parameters)
        {
            string access_token = "";

            if (Request.Query.code.HasValue)
                access_token = _OAuth.GetAccessToken(Request.Query.code);

            if (String.IsNullOrEmpty(access_token))
            {
                Request.Session[SessionKeys.FLASH_ERROR] = "There was a problem signing you in. Please try again.";
                return Response.AsRedirect("/");
            }
            else
            {
                Request.Session[SessionKeys.ACCESS_TOKEN] = access_token;
                Request.Session[SessionKeys.FLASH_SUCCESS] = "Signed in!";
                return Response.AsRedirect("/Sites");
            }
        }
    }
}