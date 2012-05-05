using System.Collections.Generic;
using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Nancy.Testing.Fakes;
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
        {
            FakeRootPathProvider.RootPath = "../../../Apphbify";    // Repoint the root path so we can find views
            _Api = api ?? new Mock<IApiService>(MockBehavior.Strict);
            _Deploy = deploy ?? new Mock<IDeploymentService>(MockBehavior.Strict);
            _OAuth = oauth ?? new Mock<IOAuth>(MockBehavior.Strict);
            _Mail = mail ?? new Mock<IMailService>(MockBehavior.Strict);
            _SessionData = sessionData;
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