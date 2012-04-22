using System;
using System.Web;
using RestSharp;

namespace Apphbify.Services
{
    public class OAuth
    {
        private string _OAuthId = "";
        private string _OAuthRedirect = "";
        private string _OAuthSecret = "";

        public OAuth(string oAuthId, string oAuthRedirect, string oAuthSecret)
        {
            _OAuthId = oAuthId;
            _OAuthRedirect = oAuthRedirect;
            _OAuthSecret = oAuthSecret;
        }

        public string GetAuthUrl()
        {
            return String.Format("https://appharbor.com/user/authorizations/new?client_id={0}&redirect_uri={1}", Uri.EscapeDataString(_OAuthId), Uri.EscapeDataString(_OAuthRedirect));
        }

        public string GetAccessToken(string code)
        {
            try
            {
                var client = new RestClient("https://appharbor.com");
                var request = new RestRequest("tokens", Method.POST)
                    .AddParameter("client_id", _OAuthId)
                    .AddParameter("client_secret", _OAuthSecret)
                    .AddParameter("code", code);
                var response = client.Execute(request);
                var parts = HttpUtility.ParseQueryString(response.Content);
                return parts["access_token"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}