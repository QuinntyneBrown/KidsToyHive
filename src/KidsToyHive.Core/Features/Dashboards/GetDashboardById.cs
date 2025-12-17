// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Dashboards;

public class GetDashboardByIdRequest : IRequest<GetDashboardByIdResponse>
{
    public Guid DashboardId { get; set; }
}
public class GetDashboardByIdResponse
{
    public DashboardDto Dashboard { get; set; }
}
public class GetDashboardByIdHandler : IRequestHandler<GetDashboardByIdRequest, GetDashboardByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetDashboardByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetDashboardByIdResponse> Handle(GetDashboardByIdRequest request, CancellationToken cancellationToken)
        => new GetDashboardByIdResponse()
        {
            Dashboard = (await _context.Dashboards.FindAsync(request.DashboardId)).ToDto()
        };
}

