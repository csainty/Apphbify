namespace Apphbify.Api
{
    public class AppHarborApi
    {
        public AppHarborApiOAuth OAuth { get; private set; }
        public AppHarborApiApplications Applications { get; private set; }

        public AppHarborApi(string oAuthId, string oAuthRedirect, string oAuthSecret)
        {
            OAuth = new AppHarborApiOAuth(oAuthId, oAuthRedirect, oAuthSecret);
            Applications = new AppHarborApiApplications();
        }
    }
}