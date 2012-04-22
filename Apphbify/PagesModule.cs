using Apphbify.ViewModels;
using Nancy;

namespace Apphbify
{
    public class PagesModule : NancyModule
    {
        private readonly DataStore _Store;

        public PagesModule(DataStore store)
        {
            _Store = store;
            Get["/"] = Home;
            Get["/Apps"] = Apps;
        }

        private Response Home(dynamic parameters)
        {
            return View["Home", new BaseViewModel("Home", Request.Session)];
        }

        private Response Apps(dynamic parameters)
        {
            return View["Apps", new AppsViewModel(_Store, Request.Session)];
        }
    }
}