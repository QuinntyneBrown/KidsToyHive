// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.SalesOrderDetails;

public class UpsertSalesOrderDetailValidator : AbstractValidator<UpsertSalesOrderDetailRequest>
{
    public UpsertSalesOrderDetailValidator()
    {
        RuleFor(request => request.SalesOrderDetail).NotNull();
        RuleFor(request => request.SalesOrderDetail).SetValidator(new SalesOrderDetailDtoValidator());
    }
}
public class UpsertSalesOrderDetailRequest : IRequest<UpsertSalesOrderDetailResponse>
{
    public SalesOrderDetailDto SalesOrderDetail { get; set; }
}
public class UpsertSalesOrderDetailResponse
{
    public Guid SalesOrderDetailId { get; set; }
}
public class UpsertSalesOrderDetailHandler : IRequestHandler<UpsertSalesOrderDetailRequest, UpsertSalesOrderDetailResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertSalesOrderDetailHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertSalesOrderDetailResponse> Handle(UpsertSalesOrderDetailRequest request, CancellationToken cancellationToken)
    {
        var salesOrderDetail = await _context.SalesOrderDetails.FindAsync(request.SalesOrderDetail.SalesOrderDetailId);
        if (salesOrderDetail == null)
        {
            salesOrderDetail = new SalesOrderDetail();
            _context.SalesOrderDetails.Add(salesOrderDetail);
        }
        salesOrderDetail.Name = request.SalesOrderDetail.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertSalesOrderDetailResponse() { SalesOrderDetailId = salesOrderDetail.SalesOrderDetailId };
    }
}

