using System.Collections.Generic;
using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Nancy.Testing.Fakes;
using Nancy.ViewEngines.Razor;
using TinyIoC;

namespace Apphbify.Tests
{
    public class TestingBootstrapper : ConfigurableBootstrapper
    {
        private readonly Mock<IApiService> _Api;
        private readonly Mock<IDeploymentService> _Deploy;
        private readonly Mock<IOAuth> _OAuth;
        private readonly Mock<IMailService> _Mail;
        private readonly Dictionary<string, object> _SessionData;

        public TestingBootstrapper(Mock<IApiService> api = null, Mock<IDeploymentService> deploy = null, Mock<IOAuth> oauth = null, Mock<IMailService> mail = null, Dictionary<string, object> sessionData = null)
            : base(Config)
        {
            // Repoint the root path so we can find views
#if DEBUG
            FakeRootPathProvider.RootPath = "../../../Apphbify";
#else
            FakeRootPathProvider.RootPath = "_PublishedWebsites/Apphbify";
#endif
            _Api = api ?? new Mock<IApiService>(MockBehavior.Strict);
            _Deploy = deploy ?? new Mock<IDeploymentService>(MockBehavior.Strict);
            _OAuth = oauth ?? new Mock<IOAuth>(MockBehavior.Strict);
            _Mail = mail ?? new Mock<IMailService>(MockBehavior.Strict);
            _SessionData = sessionData;
            SecuredPagesModule.ApiFactory = _ => _Api.Object;
            SecuredPagesModule.DeployFactory = _ => _Deploy.Object;
        }

        private static void Config(ConfigurableBoostrapperConfigurator cfg)
        {
            cfg.ViewEngine(new RazorViewEngine(new TestingRazorConfiguration()));
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            TestingSession.Enable(pipelines, _SessionData);
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            container.Register<IOAuth>(_OAuth.Object);
        }
    }
}