using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Hooks.NotifyByEmail
{
    public class When_no_payload_is_sent
    {
        private Browser _Browser;
        private BrowserResponse _Response;

        public When_no_payload_is_sent()
        {
            _Browser = Testing.CreateBrowser<HookModule>();
            _Response = _Browser.Post("/Sites/foofoo/NotifyByEmail", with =>
            {
                with.Query("email", "test@test.com");
            });
        }

        [Fact]
        public void It_should_return_bad_request()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _Response.StatusCode);
        }
    }
}