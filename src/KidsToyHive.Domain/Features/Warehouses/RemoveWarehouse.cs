using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Warehouses;

public class RemoveWarehouse
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.WarehouseId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid WarehouseId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Warehouses.FindAsync(request.WarehouseId);
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
