using System;
using System.Configuration;
using System.Reflection;
using Apphbify.Services;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Session;
using TinyIoC;

namespace Apphbify
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private byte[] _FavIcon;

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            using (var favIconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Apphbify.favicon.ico"))
            {
                _FavIcon = new byte[favIconStream.Length];
                favIconStream.Read(_FavIcon, 0, (int)favIconStream.Length);
            }
            CookieBasedSessions.Enable(pipelines);
        }

        protected override byte[] DefaultFavIcon { get { return _FavIcon ?? base.DefaultFavIcon; } }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<DataStore>(new DataStore()); // Singleton over the application lifetime

            // Choose between mail services based on whether we are live or in test.
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SENDGRID_USERNAME"]))
            {
                container.Register<IMailService, SendGridMailService>().AsMultiInstance();
            }
            else
            {
                container.Register<IMailService, NullMailService>().AsMultiInstance();
            }
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            // Singleton over the request, but only created when needed
            container.Register<OAuth>((_, __) =>
            {
                return new OAuth(ConfigurationManager.AppSettings["OAUTH_ID"], ConfigurationManager.AppSettings["OAUTH_REDIRECT"], ConfigurationManager.AppSettings["OAUTH_KEY"]);
            });
        }
    }
}