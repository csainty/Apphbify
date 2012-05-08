using System.Collections.Generic;
using Apphbify.Data;
using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Deploy
{
    public class When_deployment_succeeds
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IDeploymentService> _Deploy;

        public When_deployment_succeeds()
        {
            _Deploy = new Mock<IDeploymentService>();
            string slug;
            _Deploy.Setup(d => d.Deploy(It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug)).Returns(DeploymentResult.Success);
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
        public void It_should_redirect_to_sites()
        {
            _Response.ShouldHaveRedirectedTo("/Sites");
        }

        [Fact]
        public void It_should_have_a_success_message()
        {
            _Response.ShouldHaveSuccessMessage();
        }

        [Fact]
        public void It_should_have_fired_the_deployment()
        {
            string slug;
            _Deploy.Verify(d => d.Deploy(It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug), Times.Once());
        }
    }
}