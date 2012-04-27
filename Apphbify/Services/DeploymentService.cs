using System.Collections.Generic;
using AppHarbor;
using AppHarbor.Model;
using Apphbify.Data;

namespace Apphbify.Services
{
    public static class DeploymentService
    {
        public static DeploymentResult Deploy(AppHarborApi api, string access_token, string siteName, App application, Dictionary<string, string> variables, out string slug)
        {
            bool variablesOk = true;
            bool addonsOk = true;
            slug = "";

            // Create the application at AppHarbor and store away the slug
            var createResult = api.CreateApplication(siteName, "amazon-web-services::us-east-1");
            if (createResult.Status != CreateStatus.Created) return DeploymentResult.UnableToCreateApplication;
            slug = createResult.ID;

            // Attempt to disable precompilation. Not fatal if it fails.
            ApiService.DisablePreCompilation(access_token, slug);

            // Configure file system access
            if (application.EnableFileSystem)
                ApiService.EnableFileSystem(access_token, slug);

            // Set configuration variables
            foreach (var variable in variables)
            {
                if (api.CreateConfigurationVariable(slug, variable.Key, variable.Value).Status != CreateStatus.Created)
                    variablesOk = false;
            }

            // Deploy the first code bundle
            if (!ApiService.DeployBuild(access_token, slug, application.DownloadUrl)) return DeploymentResult.UnableToDeployCode;

            // Install addons
            foreach (var addon in application.Addons)
            {
                if (Addons.Supported.ContainsKey(addon))
                {
                    if (!ApiService.EnableAddon(access_token, slug, addon, Addons.Supported[addon]))
                        addonsOk = false;
                }
                else
                {
                    addonsOk = false;
                }
            }

            // Check for non-critical failures
            if (!variablesOk) return DeploymentResult.ErrorSettingVariables;
            if (!addonsOk) return DeploymentResult.ErrorInstallingAddons;

            return DeploymentResult.Success;
        }
    }

    public enum DeploymentResult
    {
        Success,
        UnableToCreateApplication,
        UnableToDeployCode,
        ErrorInstallingAddons,
        ErrorSettingVariables
    }
}