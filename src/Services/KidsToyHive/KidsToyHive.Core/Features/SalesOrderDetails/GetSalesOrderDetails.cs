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

namespace KidsToyHive.Core.Features.SalesOrderDetails;

public class GetSalesOrderDetailsRequest : IRequest<GetSalesOrderDetailsResponse> { }
public class GetSalesOrderDetailsResponse
{
    public IEnumerable<SalesOrderDetailDto> SalesOrderDetails { get; set; }
}
public class GetSalesOrderDetailsHandler : IRequestHandler<GetSalesOrderDetailsRequest, GetSalesOrderDetailsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetSalesOrderDetailsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetSalesOrderDetailsResponse> Handle(GetSalesOrderDetailsRequest request, CancellationToken cancellationToken)
        => new GetSalesOrderDetailsResponse()
        {
            SalesOrderDetails = await _context.SalesOrderDetails.Select(x => x.ToDto()).ToArrayAsync()
        };
}

