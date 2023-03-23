// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Api;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.Shipments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Shipments;

public class UpsertShipmentTests
{
    [Fact]
    public async Task ShouldUpsertShipment()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(UpsertShipmentTests)}:{nameof(ShouldUpsertShipment)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var upsertShipmentHandler = new UpsertShipmentHandler(context);
        }
    }
}

