using System;
using System.Collections.Generic;
using System.Net;
using AppHarbor;
using AppHarbor.Model;
using RestSharp;

namespace Apphbify.Services
{
    public class ApiService : IApiService
    {
        private readonly AppHarborApi _Api;
        private readonly string _AccessToken;

        public ApiService(string token)
        {
            _AccessToken = token;
            _Api = new AppHarborApi(new AuthInfo { AccessToken = _AccessToken, TokenType = "Bearer" });
        }

        public bool DeployBuild(string application_slug, string download_url)
        {
            var client = new RestClient("https://appharbor.com/");
            var request = new RestRequest("applications/{slug}/builds", Method.POST) { RequestFormat = DataFormat.Json }
                .AddUrlSegment("slug", application_slug)
                .AddHeader("Authorization", "BEARER " + _AccessToken)
                .AddBody(new
                {
                    branches = new
                    {
                        @default = new
                        {
                            commit_id = Guid.NewGuid().ToString().Split('-')[0],
                            commit_message = "Deployed from AppHarbify",
                            download_url = download_url
                        }
                    }
                });
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.Created;
        }

        public bool DisablePreCompilation(string application_slug)
        {
            var client = new RestClient("https://appharbor.com/");
            var request = new RestRequest("applications/{slug}/precompilation", Method.DELETE)
                .AddUrlSegment("slug", application_slug)
                .AddHeader("Authorization", "BEARER " + _AccessToken);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.NotFound;
        }

        public bool EnableFileSystem(string application_slug)
        {
            var client = new RestClient("https://appharbor.com/");
            var request = new RestRequest("applications/{slug}", Method.PUT)
                .AddUrlSegment("slug", application_slug)
                .AddHeader("Authorization", "BEARER " + _AccessToken)
                .AddParameter("Application.IsFileSystemWritable", "true");
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool EnableAddon(string application_slug, string addon_id, string plan_id)
        {
            var client = new RestClient("https://appharbor.com/");
            var request = new RestRequest("applications/{slug}/addons?addonId={addonId}&planId={planId}&termsAcknowledged=True", Method.POST)
                .AddUrlSegment("slug", application_slug)
                .AddUrlSegment("addonId", addon_id)
                .AddUrlSegment("planId", plan_id)
                .AddHeader("Authorization", "BEARER " + _AccessToken);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public IList<Application> GetApplications()
        {
            return _Api.GetApplications();
        }

        public CreateResult<long> CreateServicehook(string slug, string url)
        {
            return _Api.CreateServicehook(slug, url);
        }

        public CreateResult<string> CreateApplication(string appName, string regionId)
        {
            return _Api.CreateApplication(appName, regionId);
        }

        public CreateResult<long> CreateConfigurationVariable(string slug, string key, string value)
        {
            return _Api.CreateConfigurationVariable(slug, key, value);
        }
    }
}