// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Bookings;

public class RemoveBookingValidator : AbstractValidator<RemoveBookingRequest>
{
    public RemoveBookingValidator()
    {
        RuleFor(request => request.BookingId).NotNull();
    }
}
public class RemoveBookingRequest : IRequest
{
    public Guid BookingId { get; set; }
}
public class RemoveBookingHandler : IRequestHandler<RemoveBookingRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveBookingHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveBookingRequest request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.FindAsync(request.BookingId);
        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

