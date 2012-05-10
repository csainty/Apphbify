using Apphbify.Tests.Helpers;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.SignOut
{
    public class When_user_signs_out
    {
        private Browser _Browser;
        private BrowserResponse _Response;

        public When_user_signs_out()
        {
            _Browser = Testing.CreateBrowser<OAuthModule>(with =>
            {
                with.LoggedInUser();
            });
            _Response = _Browser.Get("/SignOut");
        }

        [Fact]
        public void It_should_clear_the_access_token()
        {
            Assert.Null(_Response.Context.Request.Session[SessionKeys.ACCESS_TOKEN]);
        }

        [Fact]
        public void It_should_redirect_home()
        {
            _Response.ShouldHaveRedirectedTo("/");
        }

        [Fact]
        public void It_should_have_a_success_message()
        {
            _Response.ShouldHaveSuccessMessage();
        }
    }
}