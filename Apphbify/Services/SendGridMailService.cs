using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Apphbify.Services
{
    public class SendGridMailService : IMailService
    {
        public void SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient("smtp.sendgrid.net", 587))
                using (var msg = new MailMessage())
                {
                    client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SENDGRID_USERNAME"], ConfigurationManager.AppSettings["SENDGRID_PASSWORD"]);

                    msg.To.Add(toAddress);
                    msg.From = new MailAddress("no-reply@appharbify.com", "AppHarbify");
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.IsBodyHtml = false;

                    client.Send(msg);
                }
            }
            catch
            {
                // TODO: Log this exception
            }
        }
    }
}