// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using KidsToyHive.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ProfessionalServiceProviders;

public class GetProfessionalServiceProviderByNameValidator : AbstractValidator<GetProfessionalServiceProviderByNameRequest>
{
    public GetProfessionalServiceProviderByNameValidator()
    {
    }
}
public class GetProfessionalServiceProviderByNameRequest : IRequest<GetProfessionalServiceProviderByNameResponse>
{
    public string FullName { get; set; }
}
public class GetProfessionalServiceProviderByNameResponse
{
    public ProfessionalServiceProviderDto ProfessionalServiceProvider { get; set; }
}
public class GetProfessionalServiceProviderByNameHandler : IRequestHandler<GetProfessionalServiceProviderByNameRequest, GetProfessionalServiceProviderByNameResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public GetProfessionalServiceProviderByNameHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetProfessionalServiceProviderByNameResponse> Handle(GetProfessionalServiceProviderByNameRequest request, CancellationToken cancellationToken)
        => new GetProfessionalServiceProviderByNameResponse()
        {
            ProfessionalServiceProvider = (await _context.ProfessionalServiceProviders.SingleAsync(x => x.FullName == request.FullName)).ToDto()
        };
}

