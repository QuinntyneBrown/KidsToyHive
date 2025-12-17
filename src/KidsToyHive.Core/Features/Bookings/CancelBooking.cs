// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using KidsToyHive.Core;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Bookings;

public class CancelBookingValidator : AbstractValidator<CancelBookingRequest>
{
    public CancelBookingValidator()
    {
    }
}
public class CancelBookingRequest : IRequest<CancelBookingResponse>
{
}
public class CancelBookingResponse
{
}
public class CancelBookingHandler : IRequestHandler<CancelBookingRequest, CancelBookingResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public CancelBookingHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<CancelBookingResponse> Handle(CancelBookingRequest request, CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
        return new CancelBookingResponse() { };
    }
}

