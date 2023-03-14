using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings;

public class GetBookingByIdRequest : IRequest<GetBookingByIdResponse>
{
    public Guid BookingId { get; set; }
}
public class GetBookingByIdResponse
{
    public BookingDto Booking { get; set; }
}
public class GetBookingByIdHandler : IRequestHandler<GetBookingByIdRequest, GetBookingByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetBookingByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetBookingByIdResponse> Handle(GetBookingByIdRequest request, CancellationToken cancellationToken)
        => new GetBookingByIdResponse()
        {
            Booking = (await _context.Bookings.FindAsync(request.BookingId)).ToDto()
        };
}
