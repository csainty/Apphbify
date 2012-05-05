using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.ResourcesTests
{
    public class StaticFileRoutes
    {
        private readonly Browser _Browser;

        public StaticFileRoutes()
        {
            _Browser = new Browser(new TestingBootstrapper());
        }

        [Fact]
        public void Should_return_favicon_ico()
        {
            var response = _Browser.Get("/favicon.ico");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void Should_return_robots_txt()
        {
            var response = _Browser.Get("/robots.txt");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void Should_return_humans_txt()
        {
            var response = _Browser.Get("/humans.txt");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void Should_return_404_on_unknown_file()
        {
            var response = _Browser.Get("/blah.txt");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}