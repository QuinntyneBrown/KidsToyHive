using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProductCategories;

public class GetProductCategoriesRequest : IRequest<GetProductCategoriesResponse> { }
public class GetProductCategoriesResponse
{
    public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }
}
public class GetProductCategoriesHandler : IRequestHandler<GetProductCategoriesRequest, GetProductCategoriesResponse>
{
    private readonly IAppDbContext _context;
    public GetProductCategoriesHandler(IAppDbContext context) => _context = context;
    public async Task<GetProductCategoriesResponse> Handle(GetProductCategoriesRequest request, CancellationToken cancellationToken)
        => new GetProductCategoriesResponse()
        {
            ProductCategories = await _context.ProductCategories.Select(x => x.ToDto()).ToArrayAsync()
        };
}
