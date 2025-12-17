// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Dashboards;

public class RemoveDashboardValidator : AbstractValidator<RemoveDashboardRequest>
{
    public RemoveDashboardValidator()
    {
        RuleFor(request => request.DashboardId).NotNull();
    }
}
public class RemoveDashboardRequest : IRequest
{
    public Guid DashboardId { get; set; }
}
public class RemoveDashboardHandler : IRequestHandler<RemoveDashboardRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveDashboardHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveDashboardRequest request, CancellationToken cancellationToken)
    {
        var dashboard = await _context.Dashboards.FindAsync(request.DashboardId);
        _context.Dashboards.Remove(dashboard);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

