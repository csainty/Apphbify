using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.OAuthCallback
{
    public class When_oauth_callback_is_received
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IOAuth> _OAuth;

        public When_oauth_callback_is_received()
        {
            _OAuth = new Mock<IOAuth>(MockBehavior.Strict);
            _OAuth.Setup(d => d.GetAccessToken(It.IsAny<string>())).Returns("12345");
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
        public void It_should_exchange_the_code()
        {
            _OAuth.Verify(d => d.GetAccessToken("testcode"), Times.Once());
        }

        [Fact]
        public void It_should_save_the_access_token()
        {
            Assert.Equal("12345", _Response.Context.Request.Session[SessionKeys.ACCESS_TOKEN]);
        }

        [Fact]
        public void It_should_redirect_to_apps()
        {
            _Response.ShouldHaveRedirectedTo("/Apps");
        }

        [Fact]
        public void It_should_have_a_success_message()
        {
            _Response.ShouldHaveSuccessMessage();
        }
    }
}