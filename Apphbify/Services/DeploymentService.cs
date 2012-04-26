using AppHarbor;
using AppHarbor.Model;
using Apphbify.Data;

namespace Apphbify.Services
{
    public static class DeploymentService
    {
        public static DeploymentResult Deploy(AppHarborApi api, string access_token, string siteName, App application, out string slug)
        {
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

            // Deploy the first code bundle
            if (!ApiService.DeployBuild(access_token, slug, application.DownloadUrl)) return DeploymentResult.UnableToDeployCode;

            // Try to install all addons, even if one fails, and report on failure at the end.
            bool addonsOk = true;
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
            if (!addonsOk) return DeploymentResult.ErrorInstallingAddons;

            return DeploymentResult.Success;
        }
    }

    public enum DeploymentResult
    {
        Success,
        UnableToCreateApplication,
        UnableToDeployCode,
        ErrorInstallingAddons
    }
}