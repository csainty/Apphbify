using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Home
{
    public class When_displaying_the_home_page
    {
        private Browser _Browser;
        private BrowserResponse _Response;

        public When_displaying_the_home_page()
        {
            _Browser = new Browser(new TestingBootstrapper());
            _Response = _Browser.Get("/");
        }

        [Fact]
        public void It_should_return_200()
        {
            Assert.Equal(HttpStatusCode.OK, _Response.StatusCode);
        }

        [Fact]
        public void It_should_render_the_master_page()
        {
            _Response.Body["a.brand"]
                .ShouldExist()
                .And.ShouldContain("AppHarbify!");
        }

        [Fact]
        public void It_should_render_the_view_content()
        {
            _Response.Body[".hero-unit h1"]
                .ShouldExist()
                .And.ShouldContain("AppHarbify all the things!");
        }
    }
}