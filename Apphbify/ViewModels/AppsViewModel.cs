using System.Collections.Generic;
using Apphbify.Data;
using Nancy.Session;

namespace Apphbify.ViewModels
{
    public class AppsViewModel : BaseViewModel
    {
        public IList<App> Apps { get; set; }

        public AppsViewModel(DataStore store, ISession session)
            : base("Apps", session)
        {
            Apps = store.GetAllApps();
        }
    }
}