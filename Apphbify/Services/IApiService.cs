namespace Apphbify.Services
{
    public interface IApiService
    {
        bool DeployBuild(string access_token, string application_slug, string download_url);

        bool DisablePreCompilation(string access_token, string application_slug);

        bool EnableAddon(string access_token, string application_slug, string addon_id, string plan_id);

        bool EnableFileSystem(string access_token, string application_slug);
    }
}