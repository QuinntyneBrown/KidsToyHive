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

namespace KidsToyHive.Core.Features.Drivers;

public class GetDriversRequest : IRequest<GetDriversResponse> { }
public class GetDriversResponse
{
    public IEnumerable<DriverDto> Drivers { get; set; }
}
public class GetDriversHandler : IRequestHandler<GetDriversRequest, GetDriversResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetDriversHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetDriversResponse> Handle(GetDriversRequest request, CancellationToken cancellationToken)
        => new GetDriversResponse()
        {
            Drivers = await _context.Drivers.Select(x => x.ToDto()).ToArrayAsync()
        };
}

