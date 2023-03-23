// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ProfessionalServiceProviders;

public class RemoveProfessionalServiceProviderValidator : AbstractValidator<RemoveProfessionalServiceProviderRequest>
{
    public RemoveProfessionalServiceProviderValidator()
    {
        RuleFor(request => request.ProfessionalServiceProviderId).NotNull();
    }
}
public class RemoveProfessionalServiceProviderRequest : IRequest
{
    public Guid ProfessionalServiceProviderId { get; set; }
}
public class RemoveProfessionalServiceProviderHandler : IRequestHandler<RemoveProfessionalServiceProviderRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveProfessionalServiceProviderHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveProfessionalServiceProviderRequest request, CancellationToken cancellationToken)
    {
        var professionalServiceProvider = await _context.ProfessionalServiceProviders.FindAsync(request.ProfessionalServiceProviderId);
        _context.ProfessionalServiceProviders.Remove(professionalServiceProvider);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

