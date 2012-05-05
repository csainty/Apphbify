using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Master
{
    public class When_no_user_is_logged_on
    {
        private readonly Browser _Browser;
        private readonly BrowserResponse _Response;

        public When_no_user_is_logged_on()
        {
            _Browser = new Browser(new TestingBootstrapper());
            _Response = _Browser.Get("/");
        }

        [Fact]
        public void It_should_render_the_sign_in_link()
        {
            _Response.Body["ul.nav.pull-right li a"]
                .ShouldExist()
                .And.ShouldContain("Sign In");
        }
    }
}