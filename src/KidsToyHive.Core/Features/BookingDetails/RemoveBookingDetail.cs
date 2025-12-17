// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.BookingDetails;

public class RemoveBookingDetailValidator : AbstractValidator<RemoveBookingDetailRequest>
{
    public RemoveBookingDetailValidator()
    {
        RuleFor(request => request.BookingDetailId).NotNull();
    }
}

public class RemoveBookingDetailRequest : IRequest
{
    public Guid BookingDetailId { get; set; }
}

public class RemoveBookingDetailHandler : IRequestHandler<RemoveBookingDetailRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveBookingDetailHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveBookingDetailRequest request, CancellationToken cancellationToken)
    {
        var bookingDetail = await _context.BookingDetails.FindAsync(request.BookingDetailId);
        _context.BookingDetails.Remove(bookingDetail);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

