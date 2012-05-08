using Apphbify.Tests.Helpers;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Deploy
{
    public class When_deploying_an_app_that_doesnt_exist
    {
        private Browser _Browser;
        private BrowserResponse _Response;

        public When_deploying_an_app_that_doesnt_exist()
        {
            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
            });
            _Response = _Browser.Post("/Deploy/foofoo");
        }

        [Fact]
        public void It_should_redirect_to_apps()
        {
            _Response.ShouldHaveRedirectedTo("/Apps");
        }

        [Fact]
        public void It_should_include_an_error()
        {
            _Response.ShouldHaveErrorMessage();
        }
    }
}