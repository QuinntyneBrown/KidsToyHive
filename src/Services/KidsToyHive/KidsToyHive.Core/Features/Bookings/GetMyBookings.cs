// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Common;
using KidsToyHive.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Bookings;

public class GetMyBookingsRequest : AuthenticatedRequest<GetMyBookingsResponse> { }
public class GetMyBookingsResponse
{
    public ICollection<BookingDto> Bookings { get; set; }
}
public class GetMyBookingsHandler : IRequestHandler<GetMyBookingsRequest, GetMyBookingsResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public GetMyBookingsHandler(IKidsToyHiveDbContext context) => _context = context;
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

