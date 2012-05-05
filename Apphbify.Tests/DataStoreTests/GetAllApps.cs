using System.Collections.Generic;
using Apphbify.Data;
using Xunit;

namespace Apphbify.Tests.DataStoreTests
{
    public class GetAllApps
    {
        IList<App> _Apps;

        public GetAllApps()
        {
            _Apps = new DataStore().GetAllApps();
        }

        [Fact]
        public void Should_contain_entries()
        {
            Assert.NotEqual(0, _Apps.Count);
        }
    }
}