namespace Apphbify.Services
{
    public interface IMailService
    {
        void SendEmail(string toAddress, string subject, string body);
    }
}