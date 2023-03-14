using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Products;

public class GetProductByIdRequest : IRequest<GetProductByIdResponse>
{
    public Guid ProductId { get; set; }
}
public class GetProductByIdResponse
{
    public ProductDto Product { get; set; }
}
public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetProductByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        => new GetProductByIdResponse()
        {
            Product = (await _context.Products
            .Include(x => x.ProductCategory)
            .Include(x => x.ProductImages)
            .ThenInclude(x => x.DigitalAsset)
            .SingleAsync(x => x.ProductId == request.ProductId)).ToDto()
        };
}
