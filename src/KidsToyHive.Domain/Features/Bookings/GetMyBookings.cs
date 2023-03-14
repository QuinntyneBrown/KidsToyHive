using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings;

public class GetMyBookingsRequest : AuthenticatedRequest<GetMyBookingsResponse> { }
public class GetMyBookingsResponse
{
    public ICollection<BookingDto> Bookings { get; set; }
}
public class GetMyBookingsHandler : IRequestHandler<GetMyBookingsRequest, GetMyBookingsResponse>
{
    public IAppDbContext _context { get; set; }
    public GetMyBookingsHandler(IAppDbContext context) => _context = context;
    public async Task<GetMyBookingsResponse> Handle(GetMyBookingsRequest request, CancellationToken cancellationToken)
    {
        var bookings = (await _context.Customers
            .Include(x => x.Bookings)
            .ThenInclude(x => x.BookingDetails)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.ProductImages)
            .ThenInclude(x => x.DigitalAsset)
            .SingleAsync(x => x.Email == request.CurrentUsername))
            .Bookings
            .Select(x => x.ToDto())
            .ToList();
        return new GetMyBookingsResponse()
        {
            Bookings = bookings
        };
    }
}
