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

namespace KidsToyHive.Core.Features.Taxes;

public class GetTaxesRequest : IRequest<GetTaxesResponse> { }
public class GetTaxesResponse
{
    public IEnumerable<TaxDto> Taxes { get; set; }
}
public class GetTaxesHandler : IRequestHandler<GetTaxesRequest, GetTaxesResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetTaxesHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetTaxesResponse> Handle(GetTaxesRequest request, CancellationToken cancellationToken)
        => new GetTaxesResponse()
        {
            Taxes = await _context.Taxes.Select(x => x.ToDto()).ToArrayAsync()
        };
}

