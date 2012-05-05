using Xunit;

namespace Apphbify.Tests.ResourcesTests
{
    public class StaticResources
    {
        [Fact]
        public void Should_find_favicon()
        {
            Assert.NotNull(Resources.StaticResources.FavIcon);
            Assert.NotEqual(0, Resources.StaticResources.FavIcon.Length);
        }

        [Fact]
        public void Should_find_robots_txt()
        {
            Assert.NotNull(Resources.StaticResources.Robots);
            Assert.NotEqual(0, Resources.StaticResources.Robots.Length);
        }

        [Fact]
        public void Should_find_humans_txt()
        {
            Assert.NotNull(Resources.StaticResources.Humans);
            Assert.NotEqual(0, Resources.StaticResources.Humans.Length);
        }
    }
}