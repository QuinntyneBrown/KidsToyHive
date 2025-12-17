// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Warehouses;

public class UpsertWarehouseValidator : AbstractValidator<UpsertWarehouseRequest>
{
    public UpsertWarehouseValidator()
    {
        RuleFor(request => request.Warehouse).NotNull();
        RuleFor(request => request.Warehouse).SetValidator(new WarehouseDtoValidator());
    }
}
public class UpsertWarehouseRequest : IRequest<UpsertWarehouseResponse>
{
    public WarehouseDto Warehouse { get; set; }
}
public class UpsertWarehouseResponse
{
    public Guid WarehouseId { get; set; }
}
public class UpsertWarehouseHandler : IRequestHandler<UpsertWarehouseRequest, UpsertWarehouseResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public UpsertWarehouseHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertWarehouseResponse> Handle(UpsertWarehouseRequest request, CancellationToken cancellationToken)
    {
        var warehouse = await _context.Warehouses.FindAsync(request.Warehouse.WarehouseId);
        if (warehouse == null)
        {
            warehouse = new Warehouse();
            _context.Warehouses.Add(warehouse);
        }
        warehouse.Name = request.Warehouse.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertWarehouseResponse() { WarehouseId = warehouse.WarehouseId };
    }
}

