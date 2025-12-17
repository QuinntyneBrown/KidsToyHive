// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Brands;

public class GetBrandsRequest : IRequest<GetBrandsResponse> { }
public class GetBrandsResponse
{
    public IEnumerable<BrandDto> Brands { get; set; }
}
public class GetBrandsHandler : IRequestHandler<GetBrandsRequest, GetBrandsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetBrandsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetBrandsResponse> Handle(GetBrandsRequest request, CancellationToken cancellationToken)
        => new GetBrandsResponse()
        {
            Brands = await _context.Brands.Select(x => x.ToDto()).ToArrayAsync()
        };
}

