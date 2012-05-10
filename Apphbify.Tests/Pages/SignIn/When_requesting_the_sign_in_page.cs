using Apphbify.Services;
using Moq;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.SignIn
{
    public class When_requesting_the_sign_in_page
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IOAuth> _OAuth;

        public When_requesting_the_sign_in_page()
        {
            _OAuth = new Mock<IOAuth>(MockBehavior.Strict);
            _OAuth.Setup(d => d.GetAuthUrl()).Returns("http://www.test.com");
            _Browser = Testing.CreateBrowser<OAuthModule>(with =>
            {
                with.OAuth(_OAuth);
            });
            _Response = _Browser.Get("/SignIn");
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
            _Response.Body[".page-header h1"]
                .ShouldExist()
                .And.ShouldContain("Authenticate with AppHarbor");
        }

        [Fact]
        public void It_should_include_the_generated_auth_url()
        {
            _Response.Body["a[href=\"http://www.test.com\"]"]
                .ShouldExist();
        }
    }
}