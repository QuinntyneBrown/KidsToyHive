using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Videos
{
    public class VideoDtoValidator: AbstractValidator<VideoDto>
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
}
