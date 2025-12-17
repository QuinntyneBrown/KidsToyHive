// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Products;

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
    private readonly IKidsToyHiveDbContext _context;
    public GetProductByIdHandler(IKidsToyHiveDbContext context) => _context = context;
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

