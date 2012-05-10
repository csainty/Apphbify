using System.Collections.Generic;
using Apphbify.Data;
using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Deploy
{
    public class When_code_deploy_fails
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IDeploymentService> _Deploy;

        public When_code_deploy_fails()
        {
            _Deploy = new Mock<IDeploymentService>(MockBehavior.Strict);
            string slug;
            _Deploy.Setup(d => d.Deploy(It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug)).Returns(DeploymentResult.UnableToDeployCode);
            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
                with.Deployment(_Deploy);
            });
            _Response = _Browser.Post("/Deploy/jabbr", with =>
            {
                with.FormValue("application_name", "JabbR Test");
            });
        }

        [Fact]
        public void It_should_redirect_to_get()
        {
            _Response.ShouldHaveRedirectedTo("/Deploy/jabbr");
        }

        [Fact]
        public void It_should_have_an_error_message()
        {
            _Response.ShouldHaveErrorMessage();
        }

        [Fact]
        public void It_should_have_fired_the_deployment()
        {
            string slug;
            _Deploy.Verify(d => d.Deploy(It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug), Times.Once());
        }
    }
}