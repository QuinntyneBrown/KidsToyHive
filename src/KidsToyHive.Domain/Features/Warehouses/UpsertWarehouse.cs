using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Warehouses;

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
    public IAppDbContext _context { get; set; }
    public UpsertWarehouseHandler(IAppDbContext context) => _context = context;
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
