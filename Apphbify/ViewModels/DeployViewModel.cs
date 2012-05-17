using System.Collections.Generic;
using Apphbify.Data;
using Nancy.Session;

namespace Apphbify.ViewModels
{
    public class DeployViewModel : BaseViewModel
    {
        public App App { get; set; }

        public IList<Region> Regions { get; set; }

        public DeployViewModel(App app, DataStore store, ISession session)
            : base("Deploy", session)
        {
            App = app;
            Regions = store.GetAllRegions();
        }
    }
}