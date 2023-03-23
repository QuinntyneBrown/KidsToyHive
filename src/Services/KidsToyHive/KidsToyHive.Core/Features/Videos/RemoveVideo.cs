// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Videos;

public class RemoveVideoValidator : AbstractValidator<RemoveVideoRequest>
{
    public RemoveVideoValidator()
    {
        RuleFor(request => request.VideoId).NotNull();
    }
}
public class RemoveVideoRequest : IRequest
{
    public Guid VideoId { get; set; }
}
public class RemoveVideoHandler : IRequestHandler<RemoveVideoRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveVideoHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveVideoRequest request, CancellationToken cancellationToken)
    {
        var video = await _context.Videos.FindAsync(request.VideoId);
        _context.Videos.Remove(video);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

