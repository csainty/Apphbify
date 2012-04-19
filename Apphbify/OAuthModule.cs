using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Nancy;
using System.Net;
using System.IO;

namespace Apphbify
{
    public class OAuthModule : NancyModule
    {
        public OAuthModule()
        {
            Get["/SignIn"] = SignIn;
            Get["/callback"] = Callback;
        }

        private Response SignIn(dynamic parameters)
        {
            string id = ConfigurationManager.AppSettings["OAUTH_ID"];
            string url = ConfigurationManager.AppSettings["OAUTH_REDIRECT"];
            return Response.AsRedirect(String.Format("https://appharbor.com/user/authorizations/new?client_id={0}&redirect_uri={1}", Uri.EscapeDataString(id), Uri.EscapeDataString(url)));
        }

        private Response Callback(dynamic parameters)
        {
            if (!Request.Query.code.HasValue)
            {
                // TODO: Add a flash message with error
                return Response.AsRedirect("/");
            }

            try
            {
                string code = Request.Query.code;
                string id = ConfigurationManager.AppSettings["OAUTH_ID"];
                string secret = ConfigurationManager.AppSettings["OAUTH_KEY"];

                var request = (HttpWebRequest)HttpWebRequest.Create("https://appharbor.com/tokens");
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                using (var stream = request.GetRequestStream())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(String.Format("client_id={0}&client_secret={1}&code={2}", Uri.EscapeDataString(id), Uri.EscapeDataString(secret), Uri.EscapeDataString(code)));
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

                var data = HttpUtility.ParseQueryString(body);
                string accessToken = data["access_token"];

                return accessToken;
            }
            catch
            {
                // TODO: Add a flash message with error
                return Response.AsRedirect("/");
            }
        }
    }
}