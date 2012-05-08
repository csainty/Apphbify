using System;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Helpers
{
    public static class BrowserResponseExtensions
    {
        public static void ShouldHaveErrorMessage(this BrowserResponse response)
        {
            Assert.True(response.Context.Request.Session.HasChanged);
            Assert.False(String.IsNullOrEmpty(response.Context.Request.Session[SessionKeys.FLASH_ERROR] as string));
        }

        public static void ShouldHaveSuccessMessage(this BrowserResponse response)
        {
            Assert.True(response.Context.Request.Session.HasChanged);
            Assert.False(String.IsNullOrEmpty(response.Context.Request.Session[SessionKeys.FLASH_SUCCESS] as string));
        }
    }
}