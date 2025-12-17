using FluentAssertions;
using KidsToyHive.Core.Models;
using KidsToyHive.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace KidsToyHive.Infrastructure.Tests.Data;

[TestFixture]
public class KidsToyHiveDbContextTests
{
    private DbContextOptions<KidsToyHiveDbContext> _options = null!;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Test]
    public void DbContext_ShouldAddBrand()
    {
        // Arrange
        using var context = new KidsToyHiveDbContext(_options, null!);
        var brand = new Brand
        {
            Name = "Test Brand",
            Description = "Test Description"
        };

        // Act
        context.Brands.Add(brand);
        context.SaveChanges();

        // Assert
        var savedBrand = context.Brands.FirstOrDefault();
        savedBrand.Should().NotBeNull();
        savedBrand!.Name.Should().Be("Test Brand");
    }

    [Test]
    public void DbContext_ShouldAddProduct()
    {
        // Arrange
        using var context = new KidsToyHiveDbContext(_options, null!);
        var product = new Product
        {
            Name = "Test Toy",
            Description = "A fun toy",
            Price = 19.99m,
            Quantity = 5
        };

        // Act
        context.Products.Add(product);
        context.SaveChanges();

        // Assert
        var savedProduct = context.Products.FirstOrDefault();
        savedProduct.Should().NotBeNull();
        savedProduct!.Name.Should().Be("Test Toy");
        savedProduct.Price.Should().Be(19.99m);
    }

    [Test]
    public void DbContext_ShouldQueryBrands()
    {
        // Arrange
        using var context = new KidsToyHiveDbContext(_options, null!);
        context.Brands.AddRange(
            new Brand { Name = "Brand 1" },
            new Brand { Name = "Brand 2" },
            new Brand { Name = "Brand 3" }
        );
        context.SaveChanges();

        // Act
        var brands = context.Brands.ToList();

        // Assert
        brands.Should().HaveCount(3);
    }

    [Test]
    public void DbContext_ShouldUpdateBrand()
    {
        // Arrange
        using var context = new KidsToyHiveDbContext(_options, null!);
        var brand = new Brand { Name = "Original Name" };
        context.Brands.Add(brand);
        context.SaveChanges();

        // Act
        brand.Name = "Updated Name";
        context.SaveChanges();

        // Assert
        var updatedBrand = context.Brands.FirstOrDefault();
        updatedBrand!.Name.Should().Be("Updated Name");
    }

    [Test]
    public void DbContext_ShouldDeleteBrand()
    {
        // Arrange
        using var context = new KidsToyHiveDbContext(_options, null!);
        var brand = new Brand { Name = "To Delete" };
        context.Brands.Add(brand);
        context.SaveChanges();

        // Act
        context.Brands.Remove(brand);
        context.SaveChanges();

        // Assert
        context.Brands.Should().BeEmpty();
    }
}
