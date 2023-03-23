// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.InventoryItems;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.InventoryItems;

public class UpsertInventoryItemTests
{
    [Fact]
    public async Task ShouldUpsertInventoryItem()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(UpsertInventoryItemTests)}:{nameof(ShouldUpsertInventoryItem)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            var upsertInventoryItemHandler = new UpsertInventoryItemHandler(context);
        }
    }
}

