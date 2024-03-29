// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Videos;

public class UpsertVideoValidator : AbstractValidator<UpsertVideoRequest>
{
    public UpsertVideoValidator()
    {
        RuleFor(request => request.Video).NotNull();
        RuleFor(request => request.Video).SetValidator(new VideoDtoValidator());
    }
}
public class UpsertVideoRequest : IRequest<UpsertVideoResponse>
{
    public VideoDto Video { get; set; }
}
public class UpsertVideoResponse
{
    public Guid VideoId { get; set; }
}
public class UpsertVideoHandler : IRequestHandler<UpsertVideoRequest, UpsertVideoResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public UpsertVideoHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertVideoResponse> Handle(UpsertVideoRequest request, CancellationToken cancellationToken)
    {
        var video = await _context.Videos.FindAsync(request.Video.VideoId);
        if (video == null)
        {
            video = new Video();
            _context.Videos.Add(video);
        }
        video.Title = request.Video.Title;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertVideoResponse() { VideoId = video.VideoId };
    }
}

