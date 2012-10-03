using Apphbify.Data;
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
            Get["/About"] = About;
        }

        private dynamic Home(dynamic parameters)
        {
            return View["Home", new BaseViewModel("Home", Request.Session)];
        }

        private dynamic Apps(dynamic parameters)
        {
            return View["Apps", new AppsViewModel(_Store, Request.Session)];
        }

        private dynamic About(dynamic parameters)
        {
            return View["About", new BaseViewModel("About", Request.Session)];
        }
    }
}