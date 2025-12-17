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

namespace KidsToyHive.Core.Features.Roles;

public class GetRolesRequest : IRequest<GetRolesResponse> { }
public class GetRolesResponse
{
    public IEnumerable<RoleDto> Roles { get; set; }
}
public class GetRolesHandler : IRequestHandler<GetRolesRequest, GetRolesResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetRolesHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetRolesResponse> Handle(GetRolesRequest request, CancellationToken cancellationToken)
        => new GetRolesResponse()
        {
            Roles = await _context.Roles.Select(x => x.ToDto()).ToArrayAsync()
        };
}

