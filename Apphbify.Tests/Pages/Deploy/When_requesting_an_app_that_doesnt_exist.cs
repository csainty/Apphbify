using Apphbify.Tests.Helpers;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Deploy
{
    public class When_requesting_an_app_that_doesnt_exist
    {
        private Browser _Browser;
        private BrowserResponse _Response;

        public When_requesting_an_app_that_doesnt_exist()
        {
            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
            });
            _Response = _Browser.Get("/Deploy/foofoo");
        }

        [Fact]
        public void It_should_redirect_to_the_apps_page()
        {
            _Response.ShouldHaveRedirectedTo("/Apps");
        }

        [Fact]
        public void It_should_add_an_error_message()
        {
            _Response.ShouldHaveErrorMessage();
        }
    }
}