using Apphbify.Data;
using Xunit;

namespace Apphbify.Tests.Bootstrapper
{
    public class DoesRegionExist
    {
        [Fact]
        public void It_should_return_true_when_region_exists()
        {
            Assert.True(new DataStore().DoesRegionExist("amazon-web-services::us-east-1"));
        }

        [Fact]
        public void It_should_return_false_when_region_does_not_exist()
        {
            Assert.False(new DataStore().DoesRegionExist("foo"));
        }
    }
}