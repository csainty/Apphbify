using Apphbify.Services;
using Nancy.Session;

namespace Apphbify.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        public string OAuthUrl { get; set; }

        public SignInViewModel(IOAuth oAuth, ISession session)
            : base("SignIn", session)
        {
            OAuthUrl = oAuth.GetAuthUrl();
        }
    }
}