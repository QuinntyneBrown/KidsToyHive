// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ProductCategories;

public class GetProductCategoryByIdRequest : IRequest<GetProductCategoryByIdResponse>
{
    public Guid ProductCategoryId { get; set; }
}
public class GetProductCategoryByIdResponse
{
    public ProductCategoryDto ProductCategory { get; set; }
}
public class GetProductCategoryByIdHandler : IRequestHandler<GetProductCategoryByIdRequest, GetProductCategoryByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetProductCategoryByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetProductCategoryByIdResponse> Handle(GetProductCategoryByIdRequest request, CancellationToken cancellationToken)
        => new GetProductCategoryByIdResponse()
        {
            ProductCategory = (await _context.ProductCategories.FindAsync(request.ProductCategoryId)).ToDto()
        };
}

