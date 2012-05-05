using Apphbify.Services;
using Moq;
using Nancy;
using TinyIoC;

namespace Apphbify.Tests
{
    public class TestingBootstrapper : DefaultNancyBootstrapper
    {
        private readonly Mock<IOAuth> _OAuth;

        public TestingBootstrapper(Mock<IOAuth> oauth = null)
        {
            _OAuth = oauth ?? DefaultIOAuth();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            container.Register<IOAuth>(_OAuth.Object);
        }

        private Mock<IOAuth> DefaultIOAuth()
        {
            var mock = new Mock<IOAuth>();
            mock.Setup(d => d.GetAuthUrl()).Returns("http://auth.com");
            mock.Setup(d => d.GetAccessToken(It.IsAny<string>())).Returns("12345");
            return mock;
        }
    }
}