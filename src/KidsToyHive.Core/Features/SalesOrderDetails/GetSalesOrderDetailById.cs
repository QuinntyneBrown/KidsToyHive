// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.SalesOrderDetails;

public class GetSalesOrderDetailByIdRequest : IRequest<GetSalesOrderDetailByIdResponse>
{
    public Guid SalesOrderDetailId { get; set; }
}
public class GetSalesOrderDetailByIdResponse
{
    public SalesOrderDetailDto SalesOrderDetail { get; set; }
}
public class GetSalesOrderDetailByIdHandler : IRequestHandler<GetSalesOrderDetailByIdRequest, GetSalesOrderDetailByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetSalesOrderDetailByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetSalesOrderDetailByIdResponse> Handle(GetSalesOrderDetailByIdRequest request, CancellationToken cancellationToken)
        => new GetSalesOrderDetailByIdResponse()
        {
            SalesOrderDetail = (await _context.SalesOrderDetails.FindAsync(request.SalesOrderDetailId)).ToDto()
        };
}

