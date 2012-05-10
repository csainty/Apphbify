using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.OAuthCallback
{
    public class When_code_can_not_be_exchanged_for_access_token
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IOAuth> _OAuth;

        public When_code_can_not_be_exchanged_for_access_token()
        {
            _OAuth = new Mock<IOAuth>(MockBehavior.Strict);
            _OAuth.Setup(d => d.GetAccessToken(It.IsAny<string>())).Returns((string)null);
            _Browser = Testing.CreateBrowser<OAuthModule>(with =>
            {
                with.OAuth(_OAuth);
            });
            _Response = _Browser.Get("/callback", with =>
            {
                with.Query("code", "testcode");
            });
        }

        [Fact]
        public void It_should_try_get_token()
        {
            _OAuth.Verify(d => d.GetAccessToken("testcode"), Times.Once());
        }

        [Fact]
        public void It_should_redirect_to_home()
        {
            _Response.ShouldHaveRedirectedTo("/");
        }

        [Fact]
        public void It_should_have_error_message()
        {
            _Response.ShouldHaveErrorMessage();
        }
    }
}