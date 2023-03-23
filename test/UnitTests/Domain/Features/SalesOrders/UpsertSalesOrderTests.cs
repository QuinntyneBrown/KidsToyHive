// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.SalesOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.SalesOrders;

public class UpsertSalesOrderTests
{
    [Fact]
    public async Task ShouldUpsertSalesOrder()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(UpsertSalesOrderTests)}:{nameof(ShouldUpsertSalesOrder)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            var upsertSalesOrderHandler = new UpsertSalesOrderHandler(context);
        }
    }
}

