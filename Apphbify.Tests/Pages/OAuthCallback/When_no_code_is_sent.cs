using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.OAuthCallback
{
    public class When_no_code_is_sent
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IOAuth> _OAuth;

        public When_no_code_is_sent()
        {
            _OAuth = new Mock<IOAuth>(MockBehavior.Strict);
            _Browser = Testing.CreateBrowser<OAuthModule>(with =>
            {
                with.OAuth(_OAuth);
            });
            _Response = _Browser.Get("/callback", with =>
            {
            });
        }

        [Fact]
        public void It_should_not_attempt_to_exchange_code()
        {
            _OAuth.Verify(d => d.GetAccessToken(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void It_should_redirect_to_home()
        {
            _Response.ShouldHaveRedirectedTo("/");
        }

        [Fact]
        public void It_should_have_an_error_message()
        {
            _Response.ShouldHaveErrorMessage();
        }
    }
}