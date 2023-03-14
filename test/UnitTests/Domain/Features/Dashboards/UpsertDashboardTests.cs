using KidsToyHive.Domain.DataAccess;
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
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"{nameof(UpsertDashboardTests)}:{nameof(ShouldUpsertDashboard)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            var upsertDashboardHandler = new UpsertDashboard.Handler(context);
        }
    }
}
