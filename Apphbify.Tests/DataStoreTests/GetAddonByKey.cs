using Apphbify.Data;
using Xunit;

namespace Apphbify.Tests.DataStoreTests
{
    public class GetAddonByKey
    {
        [Fact]
        public void It_should_fetch_a_valid_addon()
        {
            var data = new DataStore();
            var addon = data.GetAddonByKey("sqlserver");
            Assert.NotNull(addon);
            Assert.Equal("sqlserver", addon.Key);
        }

        [Fact]
        public void It_should_return_null_when_addon_not_found()
        {
            var data = new DataStore();
            var addon = data.GetAddonByKey("foofoo");
            Assert.Null(addon);
        }
    }
}