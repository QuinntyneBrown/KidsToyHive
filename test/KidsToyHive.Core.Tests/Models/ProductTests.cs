using FluentAssertions;
using KidsToyHive.Core.Models;
using NUnit.Framework;

namespace KidsToyHive.Core.Tests.Models;

[TestFixture]
public class ProductTests
{
    [Test]
    public void Product_ShouldCreateWithValidProperties()
    {
        // Arrange & Act
        var product = new Product
        {
            ProductId = 1,
            Name = "Test Toy",
            Description = "A test toy for kids",
            Price = 29.99m,
            Quantity = 10
        };

        // Assert
        product.ProductId.Should().Be(1);
        product.Name.Should().Be("Test Toy");
        product.Description.Should().Be("A test toy for kids");
        product.Price.Should().Be(29.99m);
        product.Quantity.Should().Be(10);
    }

    [Test]
    public void Product_Price_ShouldBePositive()
    {
        // Arrange
        var product = new Product { Price = 50.00m };

        // Assert
        product.Price.Should().BePositive();
    }

    [Test]
    public void Product_ShouldInheritFromBaseModel()
    {
        // Arrange
        var product = new Product();

        // Assert
        product.Should().BeAssignableTo<BaseModel>();
    }

    [TestCase(0)]
    [TestCase(10)]
    [TestCase(100)]
    public void Product_Quantity_ShouldAcceptValidValues(int quantity)
    {
        // Arrange & Act
        var product = new Product { Quantity = quantity };

        // Assert
        product.Quantity.Should().Be(quantity);
    }
}
