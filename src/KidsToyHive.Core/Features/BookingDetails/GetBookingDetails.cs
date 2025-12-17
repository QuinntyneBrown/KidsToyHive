// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.BookingDetails;

public class GetBookingDetailsRequest : IRequest<GetBookingDetailsResponse> { }
public class GetBookingDetailsResponse
{
    public IEnumerable<BookingDetailDto> BookingDetails { get; set; }
}
public class GetBookingDetailsHandler : IRequestHandler<GetBookingDetailsRequest, GetBookingDetailsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetBookingDetailsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetBookingDetailsResponse> Handle(GetBookingDetailsRequest request, CancellationToken cancellationToken)
        => new GetBookingDetailsResponse()
        {
            BookingDetails = await _context.BookingDetails.Select(x => x.ToDto()).ToArrayAsync()
        };
}

