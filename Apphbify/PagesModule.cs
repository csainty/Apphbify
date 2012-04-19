using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Nancy;

namespace Apphbify
{
    public class PagesModule : NancyModule
    {
        public PagesModule()
        {
            Get["/"] = Home;
        }

        private Response Home(dynamic parameters)
        {
            return View["Home"];
        }
    }
}