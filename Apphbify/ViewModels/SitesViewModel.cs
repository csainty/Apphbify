using System.Collections.Generic;
using AppHarbor;
using AppHarbor.Model;
using Nancy.Session;

namespace Apphbify.ViewModels
{
    public class SitesViewModel : BaseViewModel
    {
        public IList<Application> Sites { get; set; }

        public SitesViewModel(AppHarborApi api, ISession session)
            : base("Sites", session)
        {
            Sites = api.GetApplications();
        }
    }
}