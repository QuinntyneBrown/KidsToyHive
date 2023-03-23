// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Customers;

public class RemoveCustomerValidator : AbstractValidator<RemoveCustomerRequest>
{
    public RemoveCustomerValidator()
    {
        RuleFor(request => request.CustomerId).NotNull();
    }
}
public class RemoveCustomerRequest : IRequest
{
    public Guid CustomerId { get; set; }
}
public class RemoveCustomerHandler : IRequestHandler<RemoveCustomerRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveCustomerHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.CustomerId);
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

