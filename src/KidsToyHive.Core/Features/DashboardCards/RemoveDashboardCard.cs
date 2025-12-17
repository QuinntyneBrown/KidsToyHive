// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.DashboardCards;

public class RemoveDashboardCardValidator : AbstractValidator<RemoveDashboardCardRequest>
{
    public RemoveDashboardCardValidator()
    {
        RuleFor(request => request.DashboardCardId).NotNull();
    }
}
public class RemoveDashboardCardRequest : IRequest
{
    public Guid DashboardCardId { get; set; }
}
public class RemoveDashboardCardHandler : IRequestHandler<RemoveDashboardCardRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveDashboardCardHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveDashboardCardRequest request, CancellationToken cancellationToken)
    {
        var dashboardCard = await _context.DashboardCards.FindAsync(request.DashboardCardId);
        _context.DashboardCards.Remove(dashboardCard);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

