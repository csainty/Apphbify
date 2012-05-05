using System.Collections.Generic;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Master
{
    public class When_an_error_message_is_present
    {
        private readonly Browser _Browser;
        private readonly BrowserResponse _Response;

        public When_an_error_message_is_present()
        {
            _Browser = new Browser(new TestingBootstrapper(sessionData: new Dictionary<string, object>() { { SessionKeys.FLASH_ERROR, "There was an error!" } }));
            _Response = _Browser.Get("/");
        }

        [Fact]
        public void It_should_render_the_error_section()
        {
            _Response.Body["div.alert-error p"]
                .ShouldExist()
                .And.ShouldContain("There was an error!");
        }

        [Fact]
        public void It_should_have_removed_the_session_value()
        {
            Assert.True(_Response.Context.Request.Session.HasChanged);
            Assert.Null(_Response.Context.Request.Session[SessionKeys.FLASH_ERROR]);
        }
    }
}