// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ProfessionalServiceProviders;

public class GetProfessionalServiceProviderByIdRequest : IRequest<GetProfessionalServiceProviderByIdResponse>
{
    public Guid ProfessionalServiceProviderId { get; set; }
}
public class GetProfessionalServiceProviderByIdResponse
{
    public ProfessionalServiceProviderDto ProfessionalServiceProvider { get; set; }
}
public class GetProfessionalServiceProviderByIdHandler : IRequestHandler<GetProfessionalServiceProviderByIdRequest, GetProfessionalServiceProviderByIdResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public GetProfessionalServiceProviderByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetProfessionalServiceProviderByIdResponse> Handle(GetProfessionalServiceProviderByIdRequest request, CancellationToken cancellationToken)
        => new GetProfessionalServiceProviderByIdResponse()
        {
            ProfessionalServiceProvider = (await _context.ProfessionalServiceProviders.FindAsync(request.ProfessionalServiceProviderId)).ToDto()
        };
}

