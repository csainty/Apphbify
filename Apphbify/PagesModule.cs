using Apphbify.ViewModels;
using Nancy;

namespace Apphbify
{
    public class PagesModule : NancyModule
    {
        private DataStore _Store;

        public PagesModule(DataStore store)
        {
            _Store = store;
            Get["/"] = Home;
            Get["/Sites"] = Sites;
        }

        private Response Home(dynamic parameters)
        {
            return View["Home", new BaseViewModel("Home", Request.Session)];
        }

        private Response Sites(dynamic parameters)
        {
            return View["Sites", new SitesViewModel(_Store, Request.Session)];
        }
    }
}