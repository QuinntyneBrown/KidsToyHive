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

namespace KidsToyHive.Core.Features.SalesOrders;

public class GetSalesOrdersRequest : IRequest<GetSalesOrdersResponse> { }
public class GetSalesOrdersResponse
{
    public IEnumerable<SalesOrderDto> SalesOrders { get; set; }
}
public class GetSalesOrdersHandler : IRequestHandler<GetSalesOrdersRequest, GetSalesOrdersResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetSalesOrdersHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetSalesOrdersResponse> Handle(GetSalesOrdersRequest request, CancellationToken cancellationToken)
        => new GetSalesOrdersResponse()
        {
            SalesOrders = await _context.SalesOrders.Select(x => x.ToDto()).ToArrayAsync()
        };
}

