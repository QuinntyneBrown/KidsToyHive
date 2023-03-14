using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentItems;

public class UpsertShipmentItem
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.ShipmentItem).NotNull();
            RuleFor(request => request.ShipmentItem).SetValidator(new ShipmentItemDtoValidator());
        }
    }
    public class Request : IRequest<Response>
    {
        public ShipmentItemDto ShipmentItem { get; set; }
    }
    public class Response
    {
        public Guid ShipmentItemId { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        public IAppDbContext _context { get; set; }
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var shipmentItem = await _context.ShipmentItems.FindAsync(request.ShipmentItem.ShipmentItemId);
            if (shipmentItem == null)
            {
                shipmentItem = new ShipmentItem();
                _context.ShipmentItems.Add(shipmentItem);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return new Response() { ShipmentItemId = shipmentItem.ShipmentItemId };
        }
    }
}
