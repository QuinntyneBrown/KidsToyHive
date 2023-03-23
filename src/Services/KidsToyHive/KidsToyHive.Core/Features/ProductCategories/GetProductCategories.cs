// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ProductCategories;

public class GetProductCategoriesRequest : IRequest<GetProductCategoriesResponse> { }
public class GetProductCategoriesResponse
{
    public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }
}
public class GetProductCategoriesHandler : IRequestHandler<GetProductCategoriesRequest, GetProductCategoriesResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetProductCategoriesHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetProductCategoriesResponse> Handle(GetProductCategoriesRequest request, CancellationToken cancellationToken)
        => new GetProductCategoriesResponse()
        {
            ProductCategories = await _context.ProductCategories.Select(x => x.ToDto()).ToArrayAsync()
        };
}

