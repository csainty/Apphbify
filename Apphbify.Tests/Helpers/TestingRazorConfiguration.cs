using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace Apphbify.Tests.Helpers
{
    public class TestingRazorConfiguration : IRazorConfiguration
    {
        public bool AutoIncludeModelNamespace
        {
            get { return true; }
        }

        public IEnumerable<string> GetAssemblyNames()
        {
            return new[] { "AppHarbor.NET" };
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            return new string[0];
        }
    }
}