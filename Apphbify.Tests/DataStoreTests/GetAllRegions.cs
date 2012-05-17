using System.Collections.Generic;
using Apphbify.Data;
using Xunit;

namespace Apphbify.Tests.DataStoreTests
{
    public class GetAllRegions
    {
        IList<Region> _Regions;

        public GetAllRegions()
        {
            _Regions = new DataStore().GetAllRegions();
        }

        [Fact]
        public void It_should_load_some_regions()
        {
            Assert.NotEqual(0, _Regions.Count);
        }
    }
}