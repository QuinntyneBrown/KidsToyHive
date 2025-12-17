// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Taxes;

public class RemoveTaxValidator : AbstractValidator<RemoveTaxRequest>
{
    public RemoveTaxValidator()
    {
        RuleFor(request => request.TaxId).NotNull();
    }
}
public class RemoveTaxRequest : IRequest
{
    public Guid TaxId { get; set; }
}
public class RemoveTaxHandler : IRequestHandler<RemoveTaxRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveTaxHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveTaxRequest request, CancellationToken cancellationToken)
    {
        var tax = await _context.Taxes.FindAsync(request.TaxId);
        _context.Taxes.Remove(tax);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

