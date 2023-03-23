// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Core.Models;
using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core;

namespace KidsToyHive.Core.Features.Profiles;

public class RemoveProfileValidator : AbstractValidator<RemoveProfileRequest>
{
    public RemoveProfileValidator()
    {
        RuleFor(request => request.ProfileId).NotEqual(0);
    }
}
public class RemoveProfileRequest : IRequest<RemoveProfileResponse>
{
    public int ProfileId { get; set; }
}
public class RemoveProfileResponse { }
public class RemoveProfileHandler : IRequestHandler<RemoveProfileRequest, RemoveProfileResponse>
{
    private readonly IKidsToyHiveDbContext _context;

    public RemoveProfileHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<RemoveProfileResponse> Handle(RemoveProfileRequest request, CancellationToken cancellationToken)
    {
        _context.Profiles.Remove(await _context.Profiles.FindAsync(request.ProfileId));
        await _context.SaveChangesAsync(cancellationToken);
        return new RemoveProfileResponse()
        {
        };
    }
}

