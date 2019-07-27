using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Warehouses
{
    public class UpsertWarehouse
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Warehouse).NotNull();
                RuleFor(request => request.Warehouse).SetValidator(new WarehouseDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public WarehouseDto Warehouse { get; set; }
        }

        public class Response
        {
            public Guid WarehouseId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var warehouse = await _context.Warehouses.FindAsync(request.Warehouse.WarehouseId);

                if (warehouse == null) {
                    warehouse = new Warehouse();
                    _context.Warehouses.Add(warehouse);
                }

                warehouse.Name = request.Warehouse.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { WarehouseId = warehouse.WarehouseId };
            }
        }
    }
}
