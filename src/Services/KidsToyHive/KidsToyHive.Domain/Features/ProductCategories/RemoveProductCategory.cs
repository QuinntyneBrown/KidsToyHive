using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProductCategories;

public class RemoveProductCategoryValidator : AbstractValidator<RemoveProductCategoryRequest>
{
    public RemoveProductCategoryValidator()
    {
        RuleFor(request => request.ProductCategoryId).NotNull();
    }
}
public class RemoveProductCategoryRequest : IRequest
{
    public Guid ProductCategoryId { get; set; }
}
public class RemoveProductCategoryHandler : IRequestHandler<RemoveProductCategoryRequest>
{
    private readonly IAppDbContext _context;
    public RemoveProductCategoryHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var productCategory = await _context.ProductCategories.FindAsync(request.ProductCategoryId);
        _context.ProductCategories.Remove(productCategory);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}