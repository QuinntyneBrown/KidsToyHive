// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Drivers;

public class GetDriverByIdRequest : IRequest<GetDriverByIdResponse>
{
    public Guid DriverId { get; set; }
}
public class GetDriverByIdResponse
{
    public DriverDto Driver { get; set; }
}
public class GetDriverByIdHandler : IRequestHandler<GetDriverByIdRequest, GetDriverByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetDriverByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetDriverByIdResponse> Handle(GetDriverByIdRequest request, CancellationToken cancellationToken)
        => new GetDriverByIdResponse()
        {
            Driver = (await _context.Drivers.FindAsync(request.DriverId)).ToDto()
        };
}

