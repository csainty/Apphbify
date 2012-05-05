using Apphbify.Services;
using Moq;
using Nancy;
using TinyIoC;

namespace Apphbify.Tests
{
    public class TestingBootstrapper : DefaultNancyBootstrapper
    {
        private readonly Mock<IApiService> _Api;
        private readonly Mock<IDeploymentService> _Deploy;
        private readonly Mock<IOAuth> _OAuth;
        private readonly Mock<IMailService> _Mail;

        public TestingBootstrapper(Mock<IApiService> api = null, Mock<IDeploymentService> deploy = null, Mock<IOAuth> oauth = null, Mock<IMailService> mail = null)
        {
            _Api = api ?? DefaultIApi();
            _Deploy = deploy ?? DefaultIDeploy();
            _OAuth = oauth ?? DefaultIOAuth();
            _Mail = mail ?? DefaultIMail();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            container.Register<IOAuth>(_OAuth.Object);
        }

        private Mock<IOAuth> DefaultIOAuth()
        {
            var mock = new Mock<IOAuth>(MockBehavior.Strict);
            return mock;
        }

        private Mock<IApiService> DefaultIApi()
        {
            var mock = new Mock<IApiService>(MockBehavior.Strict);
            return mock;
        }

        private Mock<IDeploymentService> DefaultIDeploy()
        {
            var mock = new Mock<IDeploymentService>(MockBehavior.Strict);
            return mock;
        }

        private Mock<IMailService> DefaultIMail()
        {
            var mock = new Mock<IMailService>(MockBehavior.Strict);
            return mock;
        }
    }
}