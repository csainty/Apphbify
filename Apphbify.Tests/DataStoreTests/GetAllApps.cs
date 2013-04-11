using System.Collections.Generic;
using System.Linq;
using Apphbify.Data;
using Xunit;

namespace Apphbify.Tests.DataStoreTests
{
    public class GetAllApps
    {
        private IList<App> _Apps;

        public GetAllApps()
        {
            _Apps = new DataStore().GetAllApps();
        }

        [Fact]
        public void Should_contain_entries()
        {
            Assert.NotEqual(0, _Apps.Count);
        }

        [Fact]
        public void All_variables_should_not_be_null()
        {
            Assert.True(_Apps.All(d => d.Variables != null));
        }

        [Fact]
        public void All_addons_should_not_be_null()
        {
            Assert.True(_Apps.All(d => d.Addons != null));
        }

        [Fact]
        public void All_default_variables_should_not_be_null()
        {
            Assert.True(_Apps.All(d => d.DefaultVariables != null));
        }
    }
}