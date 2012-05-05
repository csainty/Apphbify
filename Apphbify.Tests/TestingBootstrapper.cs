using Apphbify.Services;
using Nancy;
using TinyIoC;

namespace Apphbify.Tests
{
    public class TestingBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            container.Register<IOAuth>(new OAuth("", "", ""));
        }
    }
}