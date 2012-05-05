using System.Diagnostics;

namespace Apphbify.Services
{
    public class NullMailService : IMailService
    {
        public void SendEmail(string toAddress, string subject, string body)
        {
            Debug.WriteLine("Sending email to {0}. Subject: {1}. Body: {2}.", toAddress, subject, body);
        }
    }
}