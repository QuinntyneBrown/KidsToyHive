// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Services;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.Products;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Products;

public class GetProductsTests
{
    [Fact]
    public async Task ShouldGetProducts()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(GetProductsTests)}:{nameof(ShouldGetProducts)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            context.Products.Add(new Product
            {
                Name = "Jungle Jumpaoo"
            });
            context.SaveChanges();
            var getProductsHandler = new GetProductsHandler(new InMemoryCache(), context);
            var result = await getProductsHandler.Handle(new GetProductsRequest { }, default);
            Assert.NotNull(result);
            Assert.Single(result.Products);
        }
    }
}

