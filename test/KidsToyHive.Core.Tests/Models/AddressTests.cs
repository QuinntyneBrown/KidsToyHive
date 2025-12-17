using FluentAssertions;
using KidsToyHive.Core.Models;
using NUnit.Framework;

namespace KidsToyHive.Core.Tests.Models;

[TestFixture]
public class AddressTests
{
    [Test]
    public void Address_ShouldCreateWithValidProperties()
    {
        // Arrange & Act
        var address = new Address(
            street: "123 Main St",
            city: "TestCity",
            province: "TestProvince",
            postalCode: "12345"
        );

        // Assert
        address.Street.Should().Be("123 Main St");
        address.City.Should().Be("TestCity");
        address.Province.Should().Be("TestProvince");
        address.PostalCode.Should().Be("12345");
    }

    [Test]
    public void Address_ShouldSupportLatitudeAndLongitude()
    {
        // Arrange & Act
        var address = new Address(
            street: "123 Main St",
            city: "TestCity",
            province: "TestProvince",
            postalCode: "12345",
            latitude: 40.7128,
            longitude: -74.0060
        );

        // Assert
        address.Latitude.Should().Be(40.7128);
        address.Longitude.Should().Be(-74.0060);
    }

    [Test]
    public void Address_ShouldBeValueObject()
    {
        // Arrange
        var address = new Address("123 Main St", "City", "Province", "12345");

        // Assert
        address.Should().BeAssignableTo<ValueObject>();
    }

    [Test]
    public void Address_ShouldSupportIGeoCoordinate()
    {
        // Arrange
        var address = new Address("123 Main St", "City", "Province", "12345");

        // Assert
        address.Should().BeAssignableTo<IGeoCoordinate>();
    }
}
