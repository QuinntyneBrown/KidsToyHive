// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.SalesOrderDetails;

public class RemoveSalesOrderDetailValidator : AbstractValidator<RemoveSalesOrderDetailRequest>
{
    public RemoveSalesOrderDetailValidator()
    {
        RuleFor(request => request.SalesOrderDetailId).NotNull();
    }
}
public class RemoveSalesOrderDetailRequest : IRequest
{
    public Guid SalesOrderDetailId { get; set; }
}
public class RemoveSalesOrderDetailHandler : IRequestHandler<RemoveSalesOrderDetailRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveSalesOrderDetailHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveSalesOrderDetailRequest request, CancellationToken cancellationToken)
    {
        var salesOrderDetail = await _context.SalesOrderDetails.FindAsync(request.SalesOrderDetailId);
        _context.SalesOrderDetails.Remove(salesOrderDetail);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

