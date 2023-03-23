// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.DigitalAssets;

public class GetDigitalAssetByIdRequest : IRequest<GetDigitalAssetByIdResponse>
{
    public Guid DigitalAssetId { get; set; }
}
public class GetDigitalAssetByIdResponse
{
    public DigitalAssetDto DigitalAsset { get; set; }
}
public class GetDigitalAssetByIdHandler : IRequestHandler<GetDigitalAssetByIdRequest, GetDigitalAssetByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;

    public GetDigitalAssetByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetDigitalAssetByIdResponse> Handle(GetDigitalAssetByIdRequest request, CancellationToken cancellationToken)
        => new GetDigitalAssetByIdResponse()
        {
            DigitalAsset = (await _context.DigitalAssets.FindAsync(request.DigitalAssetId)).ToDto()
        };
}

