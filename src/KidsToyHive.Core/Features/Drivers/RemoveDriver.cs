// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Drivers;

public class RemoveDriverValidator : AbstractValidator<RemoveDriverRequest>
{
    public RemoveDriverValidator()
    {
        RuleFor(request => request.DriverId).NotNull();
    }
}
public class RemoveDriverRequest : IRequest
{
    public Guid DriverId { get; set; }
}
public class RemoveDriverHandler : IRequestHandler<RemoveDriverRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveDriverHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveDriverRequest request, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers.FindAsync(request.DriverId);
        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

