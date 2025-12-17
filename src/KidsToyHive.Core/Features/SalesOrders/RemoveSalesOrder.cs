// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.SalesOrders;

public class RemoveSalesOrderValidator : AbstractValidator<RemoveSalesOrderRequest>
{
    public RemoveSalesOrderValidator()
    {
        RuleFor(request => request.SalesOrderId).NotNull();
    }
}
public class RemoveSalesOrderRequest : IRequest
{
    public Guid SalesOrderId { get; set; }
}
public class RemoveSalesOrderHandler : IRequestHandler<RemoveSalesOrderRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveSalesOrderHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveSalesOrderRequest request, CancellationToken cancellationToken)
    {
        var salesOrder = await _context.SalesOrders.FindAsync(request.SalesOrderId);
        _context.SalesOrders.Remove(salesOrder);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

