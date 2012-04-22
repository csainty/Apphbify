using System;
using System.Net;
using RestSharp;

namespace Apphbify.Services
{
    public static class BuildService
    {
        public static bool DeployBuild(string access_token, string application_slug, string download_url)
        {
            var client = new RestClient("https://appharbor.com/");
            var request = new RestRequest("applications/{slug}/builds", Method.POST) { RequestFormat = DataFormat.Json }
                .AddUrlSegment("slug", application_slug)
                .AddHeader("Authorization", "BEARER " + access_token)
                .AddBody(new
                {
                    branches = new
                    {
                        @default = new
                        {
                            commit_id = Guid.NewGuid().ToString().Split('-')[0],
                            commit_message = "Deployed from Apphbify",
                            download_url = download_url
                        }
                    }
                });
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.Created;
        }

        public static bool EnableAddon(string access_token, string application_slug, string addon_id, string plan_id)
        {
            var client = new RestClient("https://appharbor.com/");
            var request = new RestRequest("applications/{slug}/addons?addonId={addonId}&planId={planId}&termsAcknowledged=True", Method.POST)
                .AddUrlSegment("slug", application_slug)
                .AddUrlSegment("addonId", addon_id)
                .AddUrlSegment("planId", plan_id)
                .AddHeader("Authorization", "BEARER " + access_token);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}