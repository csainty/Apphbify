using System.Collections.Generic;
using Nancy.Session;

namespace Apphbify.ViewModels
{
    public class SitesViewModel : BaseViewModel
    {
        public IList<Site> Sites { get; set; }

        public SitesViewModel(DataStore store, ISession session) : base("Sites", session)
        {
            Sites = store.GetAllSites();
        }
    }
}