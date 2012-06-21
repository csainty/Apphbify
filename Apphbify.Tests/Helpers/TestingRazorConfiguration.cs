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
            return new[] { "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" };
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            return new string[0];
        }
    }
}