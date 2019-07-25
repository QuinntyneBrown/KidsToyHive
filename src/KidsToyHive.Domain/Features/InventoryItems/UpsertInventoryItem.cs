using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.InventoryItems
{
    public class UpsertInventoryItem
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.InventoryItem).NotNull();
                RuleFor(request => request.InventoryItem).SetValidator(new InventoryItemDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public InventoryItemDto InventoryItem { get; set; }
        }

        public class Response
        {
            public Guid InventoryItemId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var inventoryItem = await _context.InventoryItems.FindAsync(request.InventoryItem.InventoryItemId);

                if (inventoryItem == null) {
                    inventoryItem = new InventoryItem();
                    _context.InventoryItems.Add(inventoryItem);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { InventoryItemId = inventoryItem.InventoryItemId };
            }
        }
    }
}
