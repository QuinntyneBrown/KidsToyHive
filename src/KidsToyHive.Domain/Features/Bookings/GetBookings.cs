using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings;

public class GetBookingsRequest : IRequest<GetBookingsResponse> { }
public class GetBookingsResponse
{
    public IEnumerable<BookingDto> Bookings { get; set; }
}
public class GetBookingsHandler : IRequestHandler<GetBookingsRequest, GetBookingsResponse>
{
    private readonly IAppDbContext _context;
    public GetBookingsHandler(IAppDbContext context) => _context = context;
    public async Task<GetBookingsResponse> Handle(GetBookingsRequest request, CancellationToken cancellationToken)
        => new GetBookingsResponse()
        {
            Bookings = await _context.Bookings.Select(x => x.ToDto()).ToArrayAsync()
        };
}
