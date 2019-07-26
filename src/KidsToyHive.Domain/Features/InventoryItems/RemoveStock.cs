using FluentValidation;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.InventoryItems
{
    public class RemoveStock
    {

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : Command<Response>
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public override IEnumerable<string> SideEffects => new List<string>
            {
                $"ProductId {ProductId}"
            };
        }

        public class Response
        {
            public Guid InventoryItemId { get; set; }
            public int Version { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var inventoryItem = await _context.InventoryItems.Where(x => x.ProductId == request.ProductId).SingleAsync();

                if (request.Quantity > inventoryItem.Quantity)
                    throw new Exception();

                inventoryItem.Quantity -= request.Quantity;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    InventoryItemId = inventoryItem.InventoryItemId,
                    Version = inventoryItem.Version
                };
            }
        }
    }
}
