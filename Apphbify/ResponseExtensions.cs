using Nancy;
using Nancy.Session;

namespace Apphbify
{
    public static class ResponseExtensions
    {
        public static Response WithErrorFlash(this Response response, ISession session, string message)
        {
            session[SessionKeys.FLASH_ERROR] = message;
            return response;
        }

        public static Response WithSuccessFlash(this Response response, ISession session, string message)
        {
            session[SessionKeys.FLASH_SUCCESS] = message;
            return response;
        }
    }
}