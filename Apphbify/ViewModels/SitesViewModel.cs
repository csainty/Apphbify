using System.Collections.Generic;
using System.Linq;
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
            Sites = api.GetApplications().OrderBy(d => d.Name.ToLowerInvariant()).ToList();
        }
    }
}