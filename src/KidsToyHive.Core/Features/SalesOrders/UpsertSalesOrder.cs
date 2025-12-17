// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.SalesOrders;

public class UpsertSalesOrderValidator : AbstractValidator<UpsertSalesOrderRequest>
{
    public UpsertSalesOrderValidator()
    {
        RuleFor(request => request.SalesOrder).NotNull();
        RuleFor(request => request.SalesOrder).SetValidator(new SalesOrderDtoValidator());
    }
}
public class UpsertSalesOrderRequest : IRequest<UpsertSalesOrderResponse>
{
    public SalesOrderDto SalesOrder { get; set; }
}
public class UpsertSalesOrderResponse
{
    public Guid SalesOrderId { get; set; }
}
public class UpsertSalesOrderHandler : IRequestHandler<UpsertSalesOrderRequest, UpsertSalesOrderResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertSalesOrderHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertSalesOrderResponse> Handle(UpsertSalesOrderRequest request, CancellationToken cancellationToken)
    {
        var salesOrder = await _context.SalesOrders.FindAsync(request.SalesOrder.SalesOrderId);
        if (salesOrder == null)
        {
            salesOrder = new SalesOrder();
            _context.SalesOrders.Add(salesOrder);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertSalesOrderResponse() { SalesOrderId = salesOrder.SalesOrderId };
    }
}

