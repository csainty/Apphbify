using Xunit;

namespace Apphbify.Tests.ResourcesTests
{
    public class When_loading_static_content
    {
        [Fact]
        public void It_should_find_robots_txt()
        {
            Assert.NotNull(Resources.StaticResources.Robots);
            Assert.NotEqual(0, Resources.StaticResources.Robots.Length);
        }

        [Fact]
        public void It_should_find_humans_txt()
        {
            Assert.NotNull(Resources.StaticResources.Humans);
            Assert.NotEqual(0, Resources.StaticResources.Humans.Length);
        }
    }
}