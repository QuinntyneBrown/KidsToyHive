// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Products;

public class GetProductsRequest : IRequest<GetProductsResponse> { }
public class GetProductsResponse
{
    public IEnumerable<ProductDto> Products { get; set; }
}
public class GetProductsHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    private readonly ICache _cache;
    public GetProductsHandler(ICache cache, IKidsToyHiveDbContext context)
    {
        _context = context;
        _cache = cache;
    }
    public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken) => new GetProductsResponse()
    {
        Products = await _cache.FromCacheOrServiceAsync(() => _context.Products
        .Include(x => x.ProductCategory)
        .Include(x => x.ProductImages)
        .ThenInclude(x => x.DigitalAsset)
        .Select(x => x.ToDto()).ToArrayAsync(), "Products")
    };
}

