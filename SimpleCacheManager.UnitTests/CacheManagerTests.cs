using System;
using Ploeh.AutoFixture;
using Xunit;

namespace SimpleCacheManager.UnitTests
{
    public class CacheManagerTests
    {
        [Fact]
        public void Get_default_when_not_in_cache()
        {
            var cacheManager = new CacheManager<string>();
            string data = cacheManager.GetData("x");
            Assert.Null(data);
        }

        [Fact]
        public void Cannot_find_it_when_not_in_cache()
        {
            var cacheManager = new CacheManager<string>();
            bool contains = cacheManager.Contains("x");
            Assert.False(contains);
        }

        [Fact]
        public void Get_value_when_in_cache()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<string>();

            var cacheManager = new CacheManager<string>();
            cacheManager.Add("x", expected, new DateTime(1, 1, 1), TimeSpan.FromSeconds(1));

            string data = cacheManager.GetData("x");

            Assert.Equal(expected, data);
        }

        [Fact]
        public void Find_it_when_in_cache()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<string>();

            var cacheManager = new CacheManager<string>();
            cacheManager.Add("x", expected, new DateTime(1, 1, 1), TimeSpan.FromSeconds(1));

            bool contains = cacheManager.Contains("x");

            Assert.True(contains);
        }

        [Fact]
        public void Value_expires()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<string>();

            var cacheManager = new CacheManager<string>();
            cacheManager.Add("x1", expected, new DateTime(1, 1, 1), TimeSpan.FromSeconds(1));
            cacheManager.Add("x2", expected, new DateTime(1, 1, 2), TimeSpan.FromSeconds(1));

            Assert.False(cacheManager.Contains("x1"));
            Assert.True(cacheManager.Contains("x2"));
        }

        [Fact]
        public void Happy_path()
        {
            var cacheManager = new CacheManager<string>();
            cacheManager.Add("key1", "value1", TimeSpan.FromSeconds(300));
            cacheManager.Add("key2", "value2", TimeSpan.FromSeconds(600));

            Assert.True(cacheManager.Contains("key1"));
            Assert.True(cacheManager.Contains("key1"));

            Assert.Equal("value1", cacheManager.GetData("key1"));
            Assert.Equal("value1", cacheManager.GetData("key1"));
        }
    }
}
