using AppHarbor.Model;
using Apphbify.Services;
using Apphbify.ViewModels;
using Moq;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.AddEmailNotification
{
    public class When_enabling_a_notification
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IApiService> _Api;

        public When_enabling_a_notification()
        {
            _Api = new Mock<IApiService>(MockBehavior.Strict);
            _Api.Setup(d => d.CreateServicehook(It.IsAny<string>(), It.IsAny<string>())).Returns(new CreateResult { Status = CreateStatus.Created });

            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
                with.Api(_Api);
            });
            _Response = _Browser.Put("/Sites/testsite/Notifications/Email", ctx =>
            {
                ctx.FormValue("email", "test@test.com");
            });
        }

        [Fact]
        public void It_shoud_return_200()
        {
            Assert.Equal(HttpStatusCode.OK, _Response.StatusCode);
        }

        [Fact]
        public void It_should_create_the_service_hook()
        {
            _Api.Verify(d => d.CreateServicehook("testsite", "http://appharbify.com/Sites/testsite/NotifyByEmail?email=test%40test.com"), Times.Once());
        }

        [Fact]
        public void It_should_return_json()
        {
            var result = _Response.Body.DeserializeJson<JsonResult>();
            Assert.True(result.ok);
        }
    }
}