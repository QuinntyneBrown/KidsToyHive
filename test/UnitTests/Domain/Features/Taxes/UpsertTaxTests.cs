// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Api;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.Taxes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Taxes;

public class UpsertTaxTests
{
    [Fact]
    public async Task ShouldUpsertTax()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(UpsertTaxTests)}:{nameof(ShouldUpsertTax)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var upsertTaxHandler = new UpsertTaxHandler(context);
        }
    }
}

