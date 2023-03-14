using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Taxes;

public class Validator : AbstractValidator<Request>
{
    public Validator()
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
    public IAppDbContext _context { get; set; }
    public UpsertTaxHandler(IAppDbContext context) => _context = context;
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
