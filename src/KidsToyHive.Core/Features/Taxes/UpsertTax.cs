// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Taxes;

public class UpsertTaxValidator : AbstractValidator<UpsertTaxRequest>
{
    public UpsertTaxValidator()
    {
        RuleFor(request => request.Tax).NotNull();
        RuleFor(request => request.Tax).SetValidator(new TaxDtoValidator());
    }
}
public class UpsertTaxRequest : IRequest<UpsertTaxResponse>
{
    public TaxDto Tax { get; set; }
}
public class UpsertTaxResponse
{
    public Guid TaxId { get; set; }
}
public class UpsertTaxHandler : IRequestHandler<UpsertTaxRequest, UpsertTaxResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public UpsertTaxHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertTaxResponse> Handle(UpsertTaxRequest request, CancellationToken cancellationToken)
    {
        var tax = await _context.Taxes.FindAsync(request.Tax.TaxId);
        if (tax == null)
        {
            tax = new Tax();
            _context.Taxes.Add(tax);
        }
        tax.Rate = request.Tax.Rate;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertTaxResponse() { TaxId = tax.TaxId };
    }
}

