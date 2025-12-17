// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Taxes;

public class GetTaxByIdRequest : IRequest<GetTaxByIdResponse>
{
    public Guid TaxId { get; set; }
}
public class GetTaxByIdResponse
{
    public TaxDto Tax { get; set; }
}
public class GetTaxByIdHandler : IRequestHandler<GetTaxByIdRequest, GetTaxByIdResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public GetTaxByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetTaxByIdResponse> Handle(GetTaxByIdRequest request, CancellationToken cancellationToken)
        => new GetTaxByIdResponse()
        {
            Tax = (await _context.Taxes.FindAsync(request.TaxId)).ToDto()
        };
}

