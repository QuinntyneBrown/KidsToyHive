// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Roles;

public class GetRoleByIdRequest : IRequest<GetRoleByIdResponse>
{
    public Guid RoleId { get; set; }
}
public class GetRoleByIdResponse
{
    public RoleDto Role { get; set; }
}
public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdRequest, GetRoleByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetRoleByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetRoleByIdResponse> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
        => new GetRoleByIdResponse()
        {
            Role = (await _context.Roles.FindAsync(request.RoleId)).ToDto()
        };
}

