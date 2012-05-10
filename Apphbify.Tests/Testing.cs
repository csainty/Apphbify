using System;
using System.Collections.Generic;
using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy;
using Nancy.Testing;
using Nancy.Testing.Fakes;
using Nancy.ViewEngines.Razor;

namespace Apphbify.Tests
{
    public static class Testing
    {
        static Testing()
        {
#if DEBUG
            FakeRootPathProvider.RootPath = "../../../Apphbify";
#else
            FakeRootPathProvider.RootPath = "_PublishedWebsites/Apphbify";
#endif
        }

        public static Browser CreateBrowser<TModule>(Action<TestConfig> configBuilder = null) where TModule : NancyModule
        {
            var config = new TestConfig();
            if (configBuilder != null)
                configBuilder(config);
            var bootstrapper = new ConfigurableBootstrapper(cfg =>
            {
                cfg.Module<TModule>();
                config.Dependencies(cfg);
                cfg.DisableAutoRegistration();
                cfg.NancyEngine<NancyEngine>();
                cfg.ViewEngines(typeof(TestingViewEngine));
            });
            config.PrepareBootstrapper(bootstrapper);
            return new Browser(bootstrapper);
        }
    }

    public class TestConfig
    {
        private Mock<IApiService> _Api;
        private Mock<IDeploymentService> _Deploy;
        private Mock<IOAuth> _OAuth;
        private Mock<IMailService> _Mail;

        private Dictionary<string, object> _Session = new Dictionary<string, object>();

        public void Session(string key, object val)
        {
            _Session.Add(key, val);
        }

        public void Api(Mock<IApiService> api)
        {
            _Api = api;
        }

        public void OAuth(Mock<IOAuth> oauth)
        {
            _OAuth = oauth;
        }

        public void Deployment(Mock<IDeploymentService> deploy)
        {
            _Deploy = deploy;
        }

        public void Mail(Mock<IMailService> mail)
        {
            _Mail = mail;
        }

        public void LoggedInUser()
        {
            Session(SessionKeys.ACCESS_TOKEN, "12345");
        }

        public void Dependencies(ConfigurableBootstrapper.ConfigurableBoostrapperConfigurator cfg)
        {
            _Api = _Api ?? new Mock<IApiService>(MockBehavior.Strict);
            _Deploy = _Deploy ?? new Mock<IDeploymentService>(MockBehavior.Strict);
            _OAuth = _OAuth ?? new Mock<IOAuth>(MockBehavior.Strict);
            _Mail = _Mail ?? new Mock<IMailService>(MockBehavior.Strict);
            cfg.Dependencies(_Api.Object, _Deploy.Object, _OAuth.Object, _Mail.Object);
            SecuredPagesModule.ApiFactory = _ => _Api.Object;
            SecuredPagesModule.DeployFactory = _ => _Deploy.Object;
        }

        public void PrepareBootstrapper(ConfigurableBootstrapper bootstrapper)
        {
            TestingSession.Enable(bootstrapper, _Session);
        }
    }
}