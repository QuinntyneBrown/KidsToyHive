// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.BookingDetails;

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
    private readonly IKidsToyHiveDbContext _context;
    public GetBookingDetailByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetBookingDetailByIdResponse> Handle(GetBookingDetailByIdRequest request, CancellationToken cancellationToken)
        => new GetBookingDetailByIdResponse()
        {
            BookingDetail = (await _context.BookingDetails.FindAsync(request.BookingDetailId)).ToDto()
        };
}

