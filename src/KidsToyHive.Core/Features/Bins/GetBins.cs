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

namespace KidsToyHive.Core.Features.Bins;

public class GetBinsRequest : IRequest<GetBinsResponse> { }
public class GetBinsResponse
{
    public IEnumerable<BinDto> Bins { get; set; }
}
public class GetBinsHandler : IRequestHandler<GetBinsRequest, GetBinsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetBinsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetBinsResponse> Handle(GetBinsRequest request, CancellationToken cancellationToken)
        => new GetBinsResponse()
        {
            Bins = await _context.Bins.Select(x => x.ToDto()).ToArrayAsync()
        };
}

