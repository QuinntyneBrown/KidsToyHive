using FluentAssertions;
using KidsToyHive.Core.Services;
using Moq;
using NUnit.Framework;

namespace KidsToyHive.Core.Tests.Services;

[TestFixture]
public class InMemoryCacheTests
{
    private InMemoryCache _cache = null!;

    [SetUp]
    public void Setup()
    {
        _cache = new InMemoryCache();
    }

    [TearDown]
    public void TearDown()
    {
        _cache?.Dispose();
    }

    [Test]
    public void Set_ShouldStoreValue()
    {
        // Arrange
        var key = "test-key";
        var value = "test-value";

        // Act
        _cache.Set(key, value);
        var result = _cache.Get<string>(key);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void Get_ShouldReturnNull_WhenKeyDoesNotExist()
    {
        // Arrange
        var key = "non-existent-key";

        // Act
        var result = _cache.Get<string>(key);

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void Remove_ShouldDeleteValue()
    {
        // Arrange
        var key = "test-key";
        var value = "test-value";
        _cache.Set(key, value);

        // Act
        _cache.Remove(key);
        var result = _cache.Get<string>(key);

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void Set_ShouldUpdateExistingValue()
    {
        // Arrange
        var key = "test-key";
        var initialValue = "initial-value";
        var updatedValue = "updated-value";

        // Act
        _cache.Set(key, initialValue);
        _cache.Set(key, updatedValue);
        var result = _cache.Get<string>(key);

        // Assert
        result.Should().Be(updatedValue);
    }
}
