// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.SalesOrders;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.SalesOrders;

public class ProcessSalesOrderPaymentTests
{
    [Fact]
    public async Task ShouldProcessSalesOrderPayment()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(ProcessSalesOrderPaymentTests)}:{nameof(ShouldProcessSalesOrderPayment)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            var mockPaymentProcessor = new Mock<IPaymentProcessor>();
            var processSalesOrderPaymentHandler = new CheckoutSalesOrderHandler(context, mockPaymentProcessor.Object);
        }
    }
}

