using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Dashboards;

public class DashboardDtoValidator : AbstractValidator<DashboardDto>
{
    public DashboardDtoValidator()
    {
        RuleFor(x => x.DashboardId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}

public class DashboardDto
{
    public Guid DashboardId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}

public static class DashboardExtensions
{
    public static DashboardDto ToDto(this Dashboard dashboard)
        => new DashboardDto
        {
            DashboardId = dashboard.DashboardId,
            Name = dashboard.Name,
            Version = dashboard.Version
        };
}
