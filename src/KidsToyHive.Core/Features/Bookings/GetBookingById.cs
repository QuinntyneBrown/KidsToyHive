// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Bookings;

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
    private readonly IKidsToyHiveDbContext _context;
    public GetBookingByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetBookingByIdResponse> Handle(GetBookingByIdRequest request, CancellationToken cancellationToken)
        => new GetBookingByIdResponse()
        {
            Booking = (await _context.Bookings.FindAsync(request.BookingId)).ToDto()
        };
}

