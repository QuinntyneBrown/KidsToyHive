using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Videos;

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
    public IAppDbContext _context { get; set; }
    public UpsertVideoHandler(IAppDbContext context) => _context = context;
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
