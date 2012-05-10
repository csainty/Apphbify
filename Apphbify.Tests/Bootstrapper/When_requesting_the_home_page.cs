using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Bootstrapper
{
    public class When_requesting_the_home_page
    {
        private Browser _Browser;
        private BrowserResponse _Response;

        public When_requesting_the_home_page()
        {
            Testing.Init();
            _Browser = new Browser(new Apphbify.Bootstrapper());
            _Response = _Browser.Get("/");
        }

        [Fact]
        public void It_should_return_ok()
        {
            Assert.Equal(HttpStatusCode.OK, _Response.StatusCode);
        }
    }
}