using Apphbify.Data;
using Xunit;

namespace Apphbify.Tests.DataStoreTests
{
    public class GetAppByKey
    {
        private readonly DataStore _Data;

        public GetAppByKey()
        {
            _Data = new DataStore();
        }

        [Fact]
        public void Should_return_requested_app()
        {
            var app = _Data.GetAppByKey("jabbr");
            Assert.Equal("jabbr", app.Key);
            Assert.Equal("JabbR", app.Name);
            Assert.NotNull(app.Addons);
            Assert.NotEqual(0, app.Addons.Length);
            Assert.NotNull(app.Variables);
            Assert.NotEqual(0, app.Variables.Count);
        }

        [Fact]
        public void Should_return_null_when_app_not_found()
        {
            Assert.Null(_Data.GetAppByKey("blahblah"));
        }
    }
}