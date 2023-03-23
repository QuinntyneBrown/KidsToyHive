using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Products;

public class RemoveProductValidator : AbstractValidator<RemoveProductRequest>
{
    public RemoveProductValidator()
    {
        RuleFor(request => request.ProductId).NotNull();
    }
}
public class RemoveProductRequest : IRequest
{
    public Guid ProductId { get; set; }
}
public class RemoveProductHandler : IRequestHandler<RemoveProductRequest>
{
    private readonly IAppDbContext _context;
    public RemoveProductHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.ProductId);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
