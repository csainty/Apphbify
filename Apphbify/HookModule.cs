using System;
using Apphbify.Models;
using Apphbify.Services;
using Nancy;
using Nancy.ModelBinding;

namespace Apphbify
{
    public class HookModule : NancyModule
    {
        private IMailService _Mail;

        public HookModule(IMailService mail)
        {
            _Mail = mail;

            Post["Sites/{slug}/NotifyByEmail"] = NotifyByEmail;
        }

        public Response NotifyByEmail(dynamic parameters)
        {
            string slug = parameters.slug;
            string email = Request.Query.email;
            var data = this.Bind<ServiceHookModel>();

            if (String.IsNullOrEmpty(email))
                return HttpStatusCode.BadRequest;

            string body = String.Format("This email is to let you know a build of {0} has just completed at AppHarbor. See below for details.\n\nApplication: {0} (http://{0}.apphb.com)\nStatus: {1}\nCommit Id: {2}\nCommit Message: {3}\n\nNotifications by AppHarbify - http://appharbify.com", slug, data.Build.Status, data.Build.Commit.Id, data.Build.Commit.Message);
            string subject = String.Format("AppHarbor Build Notification - {0}: {1}", slug, data.Build.Status);
            _Mail.SendEmail(email, subject, body);

            return HttpStatusCode.OK;
        }
    }
}