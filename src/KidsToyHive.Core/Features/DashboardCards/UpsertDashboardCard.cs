// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.DashboardCards;

public class UpsertDashboardCardValidator : AbstractValidator<UpsertDashboardCardRequest>
{
    public UpsertDashboardCardValidator()
    {
        RuleFor(request => request.DashboardCard).NotNull();
        RuleFor(request => request.DashboardCard).SetValidator(new DashboardCardDtoValidator());
    }
}
public class UpsertDashboardCardRequest : IRequest<UpsertDashboardCardResponse>
{
    public DashboardCardDto DashboardCard { get; set; }
}
public class UpsertDashboardCardResponse
{
    public Guid DashboardCardId { get; set; }
}
public class UpsertDashboardCardHandler : IRequestHandler<UpsertDashboardCardRequest, UpsertDashboardCardResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertDashboardCardHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertDashboardCardResponse> Handle(UpsertDashboardCardRequest request, CancellationToken cancellationToken)
    {
        var dashboardCard = await _context.DashboardCards.FindAsync(request.DashboardCard.DashboardCardId);
        if (dashboardCard == null)
        {
            dashboardCard = new DashboardCard();
            _context.DashboardCards.Add(dashboardCard);
        }
        dashboardCard.Name = request.DashboardCard.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertDashboardCardResponse() { DashboardCardId = dashboardCard.DashboardCardId };
    }
}

