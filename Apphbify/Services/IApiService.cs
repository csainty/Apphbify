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

        IEnumerable<Application> GetApplications();

        CreateResult CreateServicehook(string slug, string url);

        CreateResult CreateApplication(string appName, string regionId);

        CreateResult CreateConfigurationVariable(string slug, string key, string value);
    }
}