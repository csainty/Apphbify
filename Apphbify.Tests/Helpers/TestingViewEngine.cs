using Nancy.ViewEngines.Razor;

namespace Apphbify.Tests.Helpers
{
    public class TestingViewEngine : RazorViewEngine
    {
        public TestingViewEngine()
            : base(new TestingRazorConfiguration())
        {
        }
    }
}