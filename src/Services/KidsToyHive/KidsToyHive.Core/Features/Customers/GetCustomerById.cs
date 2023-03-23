// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Customers;

public class GetCustomerByIdRequest : IRequest<GetCustomerByIdResponse>
{
    public Guid CustomerId { get; set; }
}
public class GetCustomerByIdResponse
{
    public CustomerDto Customer { get; set; }
}
public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetCustomerByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        => new GetCustomerByIdResponse()
        {
            Customer = (await _context.Customers
            .Include(x => x.Address)
            .Include(x => x.CustomerLocations)
            .ThenInclude(x => x.Location)
            .ThenInclude(x => x.Adddress)
            .SingleAsync(x => x.CustomerId == request.CustomerId)
            ).ToDto()
        };
}

