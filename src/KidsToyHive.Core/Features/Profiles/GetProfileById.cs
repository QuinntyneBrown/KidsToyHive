// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Core.Interfaces;
using FluentValidation;
using KidsToyHive.Core;

namespace KidsToyHive.Core.Features.Profiles;

public class GetProfileByIdValidator : AbstractValidator<GetProfileByIdRequest>
{
    public GetProfileByIdValidator()
    {
        RuleFor(request => request.ProfileId).NotEqual(0);
    }
}
public class GetProfileByIdRequest : IRequest<GetProfileByIdResponse>
{
    public int ProfileId { get; set; }
}
public class GetProfileByIdResponse
{
    public ProfileDto Profile { get; set; }
}
public class GetProfileByIdHandler : IRequestHandler<GetProfileByIdRequest, GetProfileByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;

    public GetProfileByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetProfileByIdResponse> Handle(GetProfileByIdRequest request, CancellationToken cancellationToken)
        => new GetProfileByIdResponse()
        {
            Profile = ProfileDto.FromProfile(await _context.Profiles.FindAsync(request.ProfileId))
        };
}

