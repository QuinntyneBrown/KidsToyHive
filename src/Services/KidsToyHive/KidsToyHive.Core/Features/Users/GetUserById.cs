// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Users;

public class GetUserByIdRequest : IRequest<GetUserByIdResponse>
{
    public Guid UserId { get; set; }
}
public class GetUserByIdResponse
{
    public UserDto User { get; set; }
}
public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetUserByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        => new GetUserByIdResponse()
        {
            User = (await _context.Users.FindAsync(request.UserId)).ToDto()
        };
}

