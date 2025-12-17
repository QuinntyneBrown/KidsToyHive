using FluentAssertions;
using KidsToyHive.Api.Controllers;
using KidsToyHive.Core.Features.Products;
using MediatR;
using Moq;
using NUnit.Framework;

namespace KidsToyHive.Api.Tests.Controllers;

[TestFixture]
public class ProductsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private ProductsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ProductsController(_mediatorMock.Object);
    }

    [Test]
    public async Task Get_ShouldReturnProducts()
    {
        // Arrange
        var expectedResponse = new GetProductsResponse
        {
            Products = new[]
            {
                new ProductDto { ProductId = 1, Name = "Toy 1", Price = 19.99m },
                new ProductDto { ProductId = 2, Name = "Toy 2", Price = 29.99m }
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProductsRequest>(), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Get(new GetProductsRequest());

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(expectedResponse);
        result.Value!.Products.Should().HaveCount(2);
    }

    [Test]
    public async Task GetById_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var expectedResponse = new GetProductByIdResponse
        {
            Product = new ProductDto { ProductId = productId, Name = "Test Toy", Price = 19.99m }
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetProductByIdRequest>(r => r.ProductId == productId), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetById(new GetProductByIdRequest { ProductId = productId });

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(expectedResponse);
        result.Value!.Product.ProductId.Should().Be(productId);
        result.Value.Product.Price.Should().Be(19.99m);
    }

    [Test]
    public async Task Get_ShouldCallMediator()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProductsRequest>(), default))
            .ReturnsAsync(new GetProductsResponse());

        // Act
        await _controller.Get(new GetProductsRequest());

        // Assert
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetProductsRequest>(), default), Times.Once);
    }
}
