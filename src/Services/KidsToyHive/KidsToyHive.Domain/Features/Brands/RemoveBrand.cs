using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Brands;

public class RemoveBrandValidator : AbstractValidator<RemoveBrandRequest>
{
    public RemoveBrandValidator()
    {
        RuleFor(request => request.BrandId).NotNull();
    }
}
public class RemoveBrandRequest : IRequest
{
    public Guid BrandId { get; set; }
}
public class RemoveBrandHandler : IRequestHandler<RemoveBrandRequest>
{
    private readonly IAppDbContext _context;
    public RemoveBrandHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveBrandRequest request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands.FindAsync(request.BrandId);
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
