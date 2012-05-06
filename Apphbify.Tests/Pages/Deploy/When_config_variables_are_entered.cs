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
            _Deploy = new Mock<IDeploymentService>();
            string slug;
            _Deploy.Setup(d => d.Deploy(It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug))
                .Returns(DeploymentResult.Success)
                .Callback<string, App, Dictionary<string, string>, string>((name, app, vars, alug) =>
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
                with.FormValue("auth.apiKey", "test api key");
                with.FormValue("auth.appName", "test app name");
                with.FormValue("googleAnalytics", "test analytics key");
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

        [Fact]
        public void It_should_have_sent_the_vars_to_deploy()
        {
            Assert.Equal(3, _ReceivedVars.Count);
        }
    }
}