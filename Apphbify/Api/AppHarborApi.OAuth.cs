using System;
using System.IO;
using System.Net;
using System.Web;

namespace Apphbify.Api
{
    public class AppHarborApiOAuth
    {
        private string _OAuthId = "";
        private string _OAuthRedirect = "";
        private string _OAuthSecret = "";

        public AppHarborApiOAuth(string oAuthId, string oAuthRedirect, string oAuthSecret)
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
                var request = (HttpWebRequest)HttpWebRequest.Create("https://appharbor.com/tokens");
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                using (var stream = request.GetRequestStream())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(String.Format("client_id={0}&client_secret={1}&code={2}", Uri.EscapeDataString(_OAuthId), Uri.EscapeDataString(_OAuthSecret), Uri.EscapeDataString(code)));
                    writer.Flush();
                    writer.Close();
                }

                var response = (HttpWebResponse)request.GetResponse();
                var body = "";

                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    body = reader.ReadToEnd();
                    reader.Close();
                }
                response.Close();

                var parts = HttpUtility.ParseQueryString(body);
                return parts["access_token"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}