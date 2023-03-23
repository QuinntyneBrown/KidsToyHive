// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.Dashboards;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Dashboards;

public class UpsertDashboardTests
{
    [Fact]
    public async Task ShouldUpsertDashboard()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(UpsertDashboardTests)}:{nameof(ShouldUpsertDashboard)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            var upsertDashboardHandler = new UpsertDashboardHandler(context);
        }
    }
}

