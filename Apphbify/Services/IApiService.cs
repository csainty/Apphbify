using System.Collections.Generic;
using AppHarbor.Model;

namespace Apphbify.Services
{
    public interface IApiService
    {
        bool DeployBuild(string application_slug, string download_url);

        bool DisablePreCompilation(string application_slug);

        bool EnableAddon(string application_slug, string addon_id, string plan_id);

        bool EnableFileSystem(string application_slug);

        IList<Application> GetApplications();

        CreateResult<long> CreateServicehook(string slug, string url);

        CreateResult<string> CreateApplication(string appName, string regionId);

        CreateResult<long> CreateConfigurationVariable(string slug, string key, string value);
    }
}