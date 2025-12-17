using FluentAssertions;
using KidsToyHive.Core.Models;
using NUnit.Framework;

namespace KidsToyHive.Core.Tests.Models;

[TestFixture]
public class BrandTests
{
    [Test]
    public void Brand_ShouldCreateWithValidProperties()
    {
        // Arrange & Act
        var brand = new Brand
        {
            BrandId = 1,
            Name = "TestBrand",
            Description = "Test Description"
        };

        // Assert
        brand.BrandId.Should().Be(1);
        brand.Name.Should().Be("TestBrand");
        brand.Description.Should().Be("Test Description");
    }

    [Test]
    public void Brand_ShouldInheritFromBaseModel()
    {
        // Arrange
        var brand = new Brand();

        // Assert
        brand.Should().BeAssignableTo<BaseModel>();
    }

    [Test]
    public void Brand_ShouldSetCreatedDateTime()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;
        
        // Act
        var brand = new Brand();
        brand.CreatedOn = DateTime.UtcNow;
        
        var afterCreate = DateTime.UtcNow;

        // Assert
        brand.CreatedOn.Should().BeOnOrAfter(beforeCreate);
        brand.CreatedOn.Should().BeOnOrBefore(afterCreate);
    }
}
