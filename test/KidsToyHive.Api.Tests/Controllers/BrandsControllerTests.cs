using FluentAssertions;
using KidsToyHive.Api.Controllers;
using KidsToyHive.Core.Features.Brands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace KidsToyHive.Api.Tests.Controllers;

[TestFixture]
public class BrandsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private BrandsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new BrandsController(_mediatorMock.Object);
    }

    [Test]
    public async Task Get_ShouldReturnBrands()
    {
        // Arrange
        var expectedResponse = new GetBrandsResponse
        {
            Brands = new[]
            {
                new BrandDto { BrandId = 1, Name = "Brand 1" },
                new BrandDto { BrandId = 2, Name = "Brand 2" }
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetBrandsRequest>(), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(expectedResponse);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetBrandsRequest>(), default), Times.Once);
    }

    [Test]
    public async Task GetById_ShouldReturnBrand_WhenBrandExists()
    {
        // Arrange
        var brandId = 1;
        var expectedResponse = new GetBrandByIdResponse
        {
            Brand = new BrandDto { BrandId = brandId, Name = "Test Brand" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetBrandByIdRequest>(r => r.BrandId == brandId), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetById(new GetBrandByIdRequest { BrandId = brandId });

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(expectedResponse);
        result.Value!.Brand.BrandId.Should().Be(brandId);
    }

    [Test]
    public async Task Get_ShouldCallMediator()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetBrandsRequest>(), default))
            .ReturnsAsync(new GetBrandsResponse());

        // Act
        await _controller.Get();

        // Assert
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetBrandsRequest>(), default), Times.Once);
    }
}
