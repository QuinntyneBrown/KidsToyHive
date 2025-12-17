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

namespace KidsToyHive.Core.Features.Users;

public class GetUsersRequest : IRequest<GetUsersResponse> { }
public class GetUsersResponse
{
    public IEnumerable<UserDto> Users { get; set; }
}
public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetUsersHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        => new GetUsersResponse()
        {
            Users = await _context.Users.Select(x => x.ToDto()).ToArrayAsync()
        };
}

