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

namespace KidsToyHive.Core.Features.Customers;

public class GetCustomersRequest : IRequest<GetCustomersResponse> { }
public class GetCustomersResponse
{
    public IEnumerable<CustomerDto> Customers { get; set; }
}
public class GetCustomersHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetCustomersHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        => new GetCustomersResponse()
        {
            Customers = await _context.Customers.Select(x => x.ToDto()).ToArrayAsync()
        };
}

