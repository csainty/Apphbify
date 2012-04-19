using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;

namespace Apphbify.Services
{
    public static class OAuth
    {
        public static string GetAuthUrl()
        {
            string id = ConfigurationManager.AppSettings["OAUTH_ID"];
            string url = ConfigurationManager.AppSettings["OAUTH_REDIRECT"];
            return String.Format("https://appharbor.com/user/authorizations/new?client_id={0}&redirect_uri={1}", Uri.EscapeDataString(id), Uri.EscapeDataString(url));
        }

        internal static string GetAccessToken(string code)
        {
            string id = ConfigurationManager.AppSettings["OAUTH_ID"];
            string secret = ConfigurationManager.AppSettings["OAUTH_KEY"];

            try
            {
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