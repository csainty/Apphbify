using Apphbify.Data;
using Nancy.Session;

namespace Apphbify.ViewModels
{
    public class DeployViewModel : BaseViewModel
    {
        public App App { get; set; }

        public DeployViewModel(App app, ISession session)
            : base("Deploy", session)
        {
            App = app;
        }
    }
}