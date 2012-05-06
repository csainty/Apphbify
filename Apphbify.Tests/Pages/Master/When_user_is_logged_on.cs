using System.Collections.Generic;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Master
{
    public class When_user_is_logged_on
    {
        private readonly Browser _Browser;
        private readonly BrowserResponse _Response;

        public When_user_is_logged_on()
        {
            _Browser = Testing.CreateBrowser<PagesModule>(with =>
            {
                with.LoggedInUser();
            });
            _Response = _Browser.Get("/");
        }

        [Fact]
        public void It_should_render_the_sign_out_link()
        {
            _Response.Body["ul.nav.pull-right li a"]
                .ShouldExist()
                .And.ShouldContain("Sign Out");
        }
    }
}