using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.BookingDetails;

public class GetBookingDetailByIdRequest : IRequest<GetBookingDetailByIdResponse>
{
    public Guid BookingDetailId { get; set; }
}
public class GetBookingDetailByIdResponse
{
    public BookingDetailDto BookingDetail { get; set; }
}
public class GetBookingDetailByIdHandler : IRequestHandler<GetBookingDetailByIdRequest, GetBookingDetailByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetBookingDetailByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetBookingDetailByIdResponse> Handle(GetBookingDetailByIdRequest request, CancellationToken cancellationToken)
        => new GetBookingDetailByIdResponse()
        {
            BookingDetail = (await _context.BookingDetails.FindAsync(request.BookingDetailId)).ToDto()
        };
}
