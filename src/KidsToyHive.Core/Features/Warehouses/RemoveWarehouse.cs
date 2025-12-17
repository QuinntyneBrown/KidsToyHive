// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Warehouses;

public class RemoveWarehouseValidator : AbstractValidator<RemoveWarehouseRequest>
{
    public RemoveWarehouseValidator()
    {
        RuleFor(request => request.WarehouseId).NotNull();
    }
}
public class RemoveWarehouseRequest : IRequest
{
    public Guid WarehouseId { get; set; }
}
public class RemoveWarehouseHandler : IRequestHandler<RemoveWarehouseRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveWarehouseHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveWarehouseRequest request, CancellationToken cancellationToken)
    {
        var warehouse = await _context.Warehouses.FindAsync(request.WarehouseId);
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

