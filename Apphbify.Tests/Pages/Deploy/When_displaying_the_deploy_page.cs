using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Deploy
{
    public class When_displaying_the_deploy_page
    {
        private Browser _Browser;
        private BrowserResponse _Response;

        public When_displaying_the_deploy_page()
        {
            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
            });
            _Response = _Browser.Get("/Deploy/jabbr");
        }

        [Fact]
        public void It_should_return_200()
        {
            Assert.Equal(HttpStatusCode.OK, _Response.StatusCode);
        }
    }
}