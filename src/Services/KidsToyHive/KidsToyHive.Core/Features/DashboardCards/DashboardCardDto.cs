// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.DashboardCards;

public class DashboardCardDtoValidator : AbstractValidator<DashboardCardDto>
{
    public DashboardCardDtoValidator()
    {
        RuleFor(x => x.DashboardCardId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class DashboardCardDto
{
    public Guid DashboardCardId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
public static class DashboardCardExtensions
{
    public static DashboardCardDto ToDto(this DashboardCard dashboardCard)
        => new DashboardCardDto
        {
            DashboardCardId = dashboardCard.DashboardCardId,
            Name = dashboardCard.Name,
            Version = dashboardCard.Version
        };
}

