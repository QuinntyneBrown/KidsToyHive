// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.DashboardCards;

public class GetDashboardCardByIdRequest : IRequest<GetDashboardCardByIdResponse>
{
    public Guid DashboardCardId { get; set; }
}
public class GetDashboardCardByIdResponse
{
    public DashboardCardDto DashboardCard { get; set; }
}
public class GetDashboardCardByIdHandler : IRequestHandler<GetDashboardCardByIdRequest, GetDashboardCardByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetDashboardCardByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetDashboardCardByIdResponse> Handle(GetDashboardCardByIdRequest request, CancellationToken cancellationToken)
        => new GetDashboardCardByIdResponse()
        {
            DashboardCard = (await _context.DashboardCards.FindAsync(request.DashboardCardId)).ToDto()
        };
}

