using System;
using Apphbify.Services;
using Apphbify.ViewModels;
using Moq;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.AddEmailNotification
{
    public class When_no_email_is_passed
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IApiService> _Api;

        public When_no_email_is_passed()
        {
            _Api = new Mock<IApiService>(MockBehavior.Strict);
            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
                with.Api(_Api);
            });
            _Response = _Browser.Put("/Sites/testsite/Notifications/Email");
        }

        [Fact]
        public void It_should_return_bad_request()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _Response.StatusCode);
        }

        [Fact]
        public void It_should_not_create_a_service_hook()
        {
            _Api.Verify(d => d.CreateServicehook(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void It_should_return_json()
        {
            var result = _Response.Body.DeserializeJson<JsonResult>();
            Assert.False(result.ok);
            Assert.False(String.IsNullOrEmpty(result.message));
        }
    }
}