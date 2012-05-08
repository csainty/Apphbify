using Apphbify.Models;
using Apphbify.Services;
using Moq;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Hooks.NotifyByEmail
{
    public class When_a_notification_is_sent
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IMailService> _Mail;

        public When_a_notification_is_sent()
        {
            _Mail = new Mock<IMailService>(MockBehavior.Strict);
            _Mail.Setup(d => d.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _Browser = Testing.CreateBrowser<HookModule>(with =>
            {
                with.Mail(_Mail);
            });
            _Response = _Browser.Post("/Sites/foofoo/NotifyByEmail", with =>
            {
                with.JsonBody(new ServiceHookModel { Application = new ServiceHookApplicationModel { Name = "Foo Foo" }, Build = new ServiceHookBuildModel { Status = "Succeeded", Commit = new ServiceHookCommitModel { Id = "12345", Message = "Test Message" } } });
                with.Query("email", "test@test.com");
            });
        }

        [Fact]
        public void It_should_send_the_email()
        {
            _Mail.Verify(d => d.SendEmail("test@test.com", It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void It_should_return_ok()
        {
            Assert.Equal(HttpStatusCode.OK, _Response.StatusCode);
        }
    }
}