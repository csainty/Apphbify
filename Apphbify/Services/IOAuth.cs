namespace Apphbify.Services
{
    public interface IOAuth
    {
        string GetAuthUrl();

        string GetAccessToken(string code);
    }
}