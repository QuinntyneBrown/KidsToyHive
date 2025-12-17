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

namespace KidsToyHive.Core.Features.ProfessionalServiceProviders;

public class GetProfessionalServiceProvidersRequest : IRequest<GetProfessionalServiceProvidersResponse> { }
public class GetProfessionalServiceProvidersResponse
{
    public IEnumerable<ProfessionalServiceProviderDto> ProfessionalServiceProviders { get; set; }
}
public class GetProfessionalServiceProvidersHandler : IRequestHandler<GetProfessionalServiceProvidersRequest, GetProfessionalServiceProvidersResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetProfessionalServiceProvidersHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetProfessionalServiceProvidersResponse> Handle(GetProfessionalServiceProvidersRequest request, CancellationToken cancellationToken)
        => new GetProfessionalServiceProvidersResponse()
        {
            ProfessionalServiceProviders = await _context.ProfessionalServiceProviders.Select(x => x.ToDto()).ToArrayAsync()
        };
}

