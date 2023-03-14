using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Warehouses;

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
    private readonly IAppDbContext _context;
    public RemoveWarehouseHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveWarehouseRequest request, CancellationToken cancellationToken)
    {
        var warehouse = await _context.Warehouses.FindAsync(request.WarehouseId);
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
