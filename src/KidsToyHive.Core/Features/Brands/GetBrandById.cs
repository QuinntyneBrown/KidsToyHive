// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Brands;

public class GetBrandByIdRequest : IRequest<GetBrandByIdResponse>
{
    public Guid BrandId { get; set; }
}
public class GetBrandByIdResponse
{
    public BrandDto Brand { get; set; }
}
public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdRequest, GetBrandByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetBrandByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetBrandByIdResponse> Handle(GetBrandByIdRequest request, CancellationToken cancellationToken)
        => new GetBrandByIdResponse()
        {
            Brand = (await _context.Brands.FindAsync(request.BrandId)).ToDto()
        };
}

