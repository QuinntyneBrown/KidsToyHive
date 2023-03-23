// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Drivers;

public class UpsertDriverValidator : AbstractValidator<UpsertDriverRequest>
{
    public UpsertDriverValidator()
    {
        RuleFor(request => request.Driver).NotNull();
        RuleFor(request => request.Driver).SetValidator(new DriverDtoValidator());
    }
}
public class UpsertDriverRequest : IRequest<UpsertDriverResponse>
{
    public DriverDto Driver { get; set; }
}
public class UpsertDriverResponse
{
    public Guid DriverId { get; set; }
}
public class UpsertDriverHandler : IRequestHandler<UpsertDriverRequest, UpsertDriverResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertDriverHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertDriverResponse> Handle(UpsertDriverRequest request, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers.FindAsync(request.Driver.DriverId);
        if (driver == null)
        {
            driver = new Driver();
            _context.Drivers.Add(driver);
        }
        driver.FirstName = request.Driver.FirstName;
        driver.LastName = request.Driver.LastName;
        driver.Email = request.Driver.Email;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertDriverResponse() { DriverId = driver.DriverId };
    }
}

