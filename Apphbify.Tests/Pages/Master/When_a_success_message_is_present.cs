using System.Collections.Generic;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Master
{
    public class When_a_success_message_is_present
    {
        private readonly Browser _Browser;
        private readonly BrowserResponse _Response;

        public When_a_success_message_is_present()
        {
            _Browser = Testing.CreateBrowser<PagesModule>(with =>
            {
                with.Session(SessionKeys.FLASH_SUCCESS, "All done!");
            });
            _Response = _Browser.Get("/");
        }

        [Fact]
        public void It_should_render_the_success_section()
        {
            _Response.Body["div.alert-success p"]
                .ShouldExist()
                .And.ShouldContain("All done!");
        }

        [Fact]
        public void It_should_have_removed_the_session_value()
        {
            Assert.True(_Response.Context.Request.Session.HasChanged);
            Assert.Null(_Response.Context.Request.Session[SessionKeys.FLASH_SUCCESS]);
        }
    }
}