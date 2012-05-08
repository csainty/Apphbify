using Apphbify.Tests.Helpers;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Deploy
{
    public class When_no_application_name_is_specified
    {
        private Browser _Browser;
        private BrowserResponse _Response;

        public When_no_application_name_is_specified()
        {
            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
            });
            _Response = _Browser.Post("/Deploy/jabbr");
        }

        [Fact]
        public void It_should_redirect_to_get()
        {
            _Response.ShouldHaveRedirectedTo("/Deploy/jabbr");
        }

        [Fact]
        public void It_should_include_an_error()
        {
            _Response.ShouldHaveErrorMessage();
        }
    }
}