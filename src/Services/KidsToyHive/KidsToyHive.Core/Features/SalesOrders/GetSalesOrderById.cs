// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.SalesOrders;

public class GetSalesOrderByIdRequest : IRequest<GetSalesOrderByIdResponse>
{
    public Guid SalesOrderId { get; set; }
}
public class GetSalesOrderByIdResponse
{
    public SalesOrderDto SalesOrder { get; set; }
}
public class GetSalesOrderByIdHandler : IRequestHandler<GetSalesOrderByIdRequest, GetSalesOrderByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetSalesOrderByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetSalesOrderByIdResponse> Handle(GetSalesOrderByIdRequest request, CancellationToken cancellationToken)
        => new GetSalesOrderByIdResponse()
        {
            SalesOrder = (await _context.SalesOrders.FindAsync(request.SalesOrderId)).ToDto()
        };
}

