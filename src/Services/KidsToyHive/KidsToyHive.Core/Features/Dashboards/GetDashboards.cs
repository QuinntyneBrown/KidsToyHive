// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Dashboards;

public class GetDashboardsRequest : IRequest<GetDashboardsResponse> { }
public class GetDashboardsResponse
{
    public IEnumerable<DashboardDto> Dashboards { get; set; }
}
public class GetDashboardsHandler : IRequestHandler<GetDashboardsRequest, GetDashboardsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetDashboardsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetDashboardsResponse> Handle(GetDashboardsRequest request, CancellationToken cancellationToken)
        => new GetDashboardsResponse()
        {
            Dashboards = await _context.Dashboards.Select(x => x.ToDto()).ToArrayAsync()
        };
}

