using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentBookings;

public class GetShipmentBookingByIdRequest : IRequest<GetShipmentBookingByIdResponse>
{
    public Guid ShipmentBookingId { get; set; }
}
public class GetShipmentBookingByIdResponse
{
    public ShipmentBookingDto ShipmentBooking { get; set; }
}
public class GetShipmentBookingByIdHandler : IRequestHandler<GetShipmentBookingByIdRequest, GetShipmentBookingByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetShipmentBookingByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetShipmentBookingByIdResponse> Handle(GetShipmentBookingByIdRequest request, CancellationToken cancellationToken)
        => new GetShipmentBookingByIdResponse()
        {
            ShipmentBooking = (await _context.ShipmentBookings.FindAsync(request.ShipmentBookingId)).ToDto()
        };
}
