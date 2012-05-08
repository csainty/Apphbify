using System;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.ResourcesTests
{
    public class When_requesting_static_content
    {
        private readonly Browser _Browser;

        public When_requesting_static_content()
        {
            _Browser = Testing.CreateBrowser<PagesModule>();
        }

        [Fact]
        public void It_should_return_favicon_ico()
        {
            var response = _Browser.Get("/favicon.ico");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(String.IsNullOrEmpty(response.Body.AsString()));
        }

        [Fact]
        public void It_should_return_robots_txt()
        {
            var response = _Browser.Get("/robots.txt");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(String.IsNullOrEmpty(response.Body.AsString()));
        }

        [Fact]
        public void It_should_return_humans_txt()
        {
            var response = _Browser.Get("/humans.txt");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(String.IsNullOrEmpty(response.Body.AsString()));
        }

        [Fact]
        public void It_should_return_404_on_unknown_file()
        {
            var response = _Browser.Get("/blah.txt");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}