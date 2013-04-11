using System.Collections.Generic;
using Apphbify.Data;
using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Deploy
{
    public class When_config_variables_are_entered
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IDeploymentService> _Deploy;
        private Dictionary<string, string> _ReceivedVars;

        public When_config_variables_are_entered()
        {
            _Deploy = new Mock<IDeploymentService>(MockBehavior.Strict);
            string slug;
            _Deploy.Setup(d => d.Deploy(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug))
                .Returns(DeploymentResult.Success)
                .Callback<string, string, App, Dictionary<string, string>, string>((name, region, app, vars, alug) =>
                {
                    _ReceivedVars = vars;
                }); ;
            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
                with.Deployment(_Deploy);
            });
            _Response = _Browser.Post("/Deploy/jabbr", with =>
            {
                with.FormValue("application_name", "JabbR Test");
                with.FormValue("region_id", "amazon-web-services::us-east-1");
                with.FormValue("jabbr:googleAnalytics", "test analytics key");
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
            _Deploy.Verify(d => d.Deploy(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug), Times.Once());
        }

        [Fact]
        public void It_should_have_sent_the_vars_to_deploy()
        {
            Assert.Equal(5, _ReceivedVars.Count);
        }

        [Fact]
        public void The_variables_should_be_correct()
        {
            Assert.Equal("test analytics key", _ReceivedVars["jabbr:googleAnalytics"]);
        }
    }
}