using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Dashboards;

public class DashboardOptionsDtoValidator : AbstractValidator<DashboardOptionsDto>
{
    public DashboardOptionsDtoValidator()
    {
    }
}
public class DashboardOptionsDto
{
}
