using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentBookings;

public class GetShipmentBookingsRequest : IRequest<GetShipmentBookingsResponse> { }
public class GetShipmentBookingsResponse
{
    public IEnumerable<ShipmentBookingDto> ShipmentBookings { get; set; }
}
public class GetShipmentBookingsHandler : IRequestHandler<GetShipmentBookingsRequest, GetShipmentBookingsResponse>
{
    private readonly IAppDbContext _context;
    public GetShipmentBookingsHandler(IAppDbContext context) => _context = context;
    public async Task<GetShipmentBookingsResponse> Handle(GetShipmentBookingsRequest request, CancellationToken cancellationToken)
        => new GetShipmentBookingsResponse()
        {
            ShipmentBookings = await _context.ShipmentBookings.Select(x => x.ToDto()).ToArrayAsync()
        };
}
