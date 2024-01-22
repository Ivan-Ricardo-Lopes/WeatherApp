using IvanLopes.Forecast.Infra.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace IvanLopes.Forecast.Tests.Infra
{
    public class InMemoryCacheServiceTests
    {
        private readonly IMemoryCache _memoryCache;
        private readonly InMemoryCacheService _cacheService;

        public InMemoryCacheServiceTests()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cacheService = new InMemoryCacheService(_memoryCache);
        }

        [Fact]
        public void Get_ReturnsValue_WhenKeyExists()
        {
            // Arrange
            string key = "testKey";
            string? expectedValue = "testValue";
            _memoryCache.Set(key, expectedValue);

            // Act
            var result = _cacheService.Get<string>(key);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Get_ReturnsNull_WhenKeyDoesNotExist()
        {
            // Arrange
            string key = "nonExistingKey";

            // Act
            var result = _cacheService.Get<string>(key);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Set_AddsValueToCache_WhenKeyDoesNotExist()
        {
            // Arrange
            string key = "testKey2";
            string? expectedValue = "testValue";

            // Act
            _cacheService.Set(key, expectedValue);
            var result = _memoryCache.Get<string>(key);

            // Assert
            Assert.Equal(expectedValue, result);
        }

    }

}
