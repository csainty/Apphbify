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
    }
}