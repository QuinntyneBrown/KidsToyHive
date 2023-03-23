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

namespace KidsToyHive.Core.Features.DashboardCards;

public class GetDashboardCardsRequest : IRequest<GetDashboardCardsResponse> { }
public class GetDashboardCardsResponse
{
    public IEnumerable<DashboardCardDto> DashboardCards { get; set; }
}
public class GetDashboardCardsHandler : IRequestHandler<GetDashboardCardsRequest, GetDashboardCardsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetDashboardCardsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetDashboardCardsResponse> Handle(GetDashboardCardsRequest request, CancellationToken cancellationToken)
        => new GetDashboardCardsResponse()
        {
            DashboardCards = await _context.DashboardCards.Select(x => x.ToDto()).ToArrayAsync()
        };
}

