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

namespace KidsToyHive.Core.Features.Bookings;

public class GetBookingsRequest : IRequest<GetBookingsResponse> { }
public class GetBookingsResponse
{
    public IEnumerable<BookingDto> Bookings { get; set; }
}
public class GetBookingsHandler : IRequestHandler<GetBookingsRequest, GetBookingsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetBookingsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetBookingsResponse> Handle(GetBookingsRequest request, CancellationToken cancellationToken)
        => new GetBookingsResponse()
        {
            Bookings = await _context.Bookings.Select(x => x.ToDto()).ToArrayAsync()
        };
}

