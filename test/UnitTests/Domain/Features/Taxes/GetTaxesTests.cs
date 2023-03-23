// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Api;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.Taxes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Taxes;

public class GetTaxesTests
{
    [Fact]
    public async Task ShouldGetTaxes()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(GetTaxesTests)}:{nameof(ShouldGetTaxes)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var getTaxesHandler = new GetTaxesHandler(context);
            var result = await getTaxesHandler.Handle(new GetTaxesRequest { }, default);
            Assert.Single(result.Taxes);
            Assert.Equal(.13, result.Taxes.First().Rate);
        }
    }
}

