using System;
using System.Configuration;
using System.Text;
using Apphbify.Data;
using Apphbify.Services;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Cryptography;
using Nancy.Session;
using TinyIoC;

namespace Apphbify
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private static CryptographyConfiguration _CryptographyConfiguration = CreateCrypto();

        protected override CryptographyConfiguration CryptographyConfiguration { get { return _CryptographyConfiguration; } }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            CookieBasedSessions.Enable(pipelines);
        }

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
            container.Register<IOAuth>((_, __) =>
            {
                return new OAuth(ConfigurationManager.AppSettings["OAUTH_ID"], ConfigurationManager.AppSettings["OAUTH_REDIRECT"], ConfigurationManager.AppSettings["OAUTH_KEY"]);
            });
            container.Register<IApiService>((_, __) =>
            {
                if (context.Request == null || context.Request.Session == null || context.Request.Session[SessionKeys.ACCESS_TOKEN] == null)
                    return null;
                return new ApiService((string)context.Request.Session[SessionKeys.ACCESS_TOKEN]);
            });
            container.Register<IDeploymentService, DeploymentService>().AsSingleton();
        }

        private static CryptographyConfiguration CreateCrypto()
        {
            string passphrase = ConfigurationManager.AppSettings["CRYPTO_PASSPHRASE"];
            byte[] salt = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["CRYPTO_SALT"]);
            var keygen = new PassphraseKeyGenerator(passphrase, salt);
            return new CryptographyConfiguration(new RijndaelEncryptionProvider(keygen), new DefaultHmacProvider(keygen));
        }
    }
}