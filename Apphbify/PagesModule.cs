using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Nancy;

namespace Apphbify
{
    public class PagesModule : NancyModule
    {
        public PagesModule()
        {
            Get["/"] = Home;
            Get["/Sites"] = Sites;
        }

        private Response Home(dynamic parameters)
        {
            return View["Home", BaseModel()];
        }

        private Response Sites(dynamic parameters)
        {
            return View["Sites", BaseModel()];
        }

        private object BaseModel()
        {
            string error_flash = Request.Session[SessionKeys.FLASH_ERROR] as string;
            string success_flash= Request.Session[SessionKeys.FLASH_SUCCESS] as string;
            Request.Session.Delete(SessionKeys.FLASH_ERROR);
            Request.Session.Delete(SessionKeys.FLASH_SUCCESS);

            var model = new
            {
                IsLoggedOn = Request.Session[SessionKeys.ACCESS_TOKEN] != null,
                ErrorFlash = error_flash ?? "",
                SuccessFlash = success_flash ?? "",
                HasErrorFlash = !String.IsNullOrWhiteSpace(error_flash),
                HasSuccessFlash= !String.IsNullOrWhiteSpace(success_flash)
            };
            return model;
        }
    }
}