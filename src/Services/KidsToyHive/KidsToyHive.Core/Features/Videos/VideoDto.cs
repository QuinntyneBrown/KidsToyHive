// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.Videos;

public class VideoDtoValidator : AbstractValidator<VideoDto>
{
    public VideoDtoValidator()
    {
        RuleFor(x => x.VideoId).NotNull();
        RuleFor(x => x.Title).NotNull();
    }
}
public class VideoDto
{
    public Guid VideoId { get; set; }
    public string Title { get; set; }
    public int Version { get; set; }
}
public static class VideoExtensions
{
    public static VideoDto ToDto(this Video video)
        => new VideoDto
        {
            VideoId = video.VideoId,
            Title = video.Title,
            Version = video.Version
        };
}

