// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Bins;

public class GetBinByIdRequest : IRequest<GetBinByIdResponse>
{
    public Guid BinId { get; set; }
}
public class GetBinByIdResponse
{
    public BinDto Bin { get; set; }
}
public class GetBinByIdHandler : IRequestHandler<GetBinByIdRequest, GetBinByIdResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public GetBinByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetBinByIdResponse> Handle(GetBinByIdRequest request, CancellationToken cancellationToken)
        => new GetBinByIdResponse()
        {
            Bin = (await _context.Bins.FindAsync(request.BinId)).ToDto()
        };
}

