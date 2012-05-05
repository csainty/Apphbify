using System;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Sites
{
    public class When_an_anonymous_user_calls_sites
    {
        public readonly Browser _Browser;
        public readonly BrowserResponse _Response;

        public When_an_anonymous_user_calls_sites()
        {
            _Browser = new Browser(new TestingBootstrapper());
            _Response = _Browser.Get("/Sites");
        }

        [Fact]
        public void It_should_redirect_to_sign_on()
        {
            _Response.ShouldHaveRedirectedTo("/SignIn?redirect=" + Uri.EscapeDataString("/Sites"));
        }
    }
}