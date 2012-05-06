using System.Collections.Generic;
using AppHarbor.Model;
using Apphbify.Services;
using Moq;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Sites
{
    public class When_displaying_the_sites_page
    {
        private readonly Browser _Browser;
        private readonly BrowserResponse _Response;

        public When_displaying_the_sites_page()
        {
            var api = new Mock<IApiService>();
            api.Setup(d => d.GetApplications()).Returns(new List<Application> { new Application { Slug = "test", Name = "My Test Site" } });

            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.Session(SessionKeys.ACCESS_TOKEN, "12345");
                with.Api(api);
            });
            _Response = _Browser.Get("/Sites");
        }

        [Fact]
        public void It_should_return_200()
        {
            Assert.Equal(HttpStatusCode.OK, _Response.StatusCode);
        }

        [Fact]
        public void It_should_render_the_master_page()
        {
            _Response.Body["a.brand"]
                .ShouldExist()
                .And.ShouldContain("AppHarbify!");
        }

        [Fact]
        public void It_should_render_the_view_content()
        {
            _Response.Body[".page-header h1"]
                .ShouldExist()
                .And.ShouldContain("Deployed Sites");
        }

        [Fact]
        public void It_should_render_table_rows()
        {
            _Response.Body["tr.site"]
                .ShouldExist();
        }
    }
}