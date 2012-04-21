using System.Configuration;
using Apphbify.Api;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Session;
using TinyIoC;

namespace Apphbify
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            CookieBasedSessions.Enable(pipelines);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<DataStore>(new DataStore()); // Singleton over the application lifetime
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            // Singleton over the request, but only created when needed
            container.Register<AppHarborApi>((_, __) =>
            {
                return new AppHarborApi(ConfigurationManager.AppSettings["OAUTH_ID"], ConfigurationManager.AppSettings["OAUTH_REDIRECT"], ConfigurationManager.AppSettings["OAUTH_KEY"]);
            });
        }
    }
}