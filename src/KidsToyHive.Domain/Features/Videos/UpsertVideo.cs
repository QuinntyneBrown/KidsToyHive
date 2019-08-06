using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Videos
{
    public class UpsertVideo
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Video).NotNull();
                RuleFor(request => request.Video).SetValidator(new VideoDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public VideoDto Video { get; set; }
        }

        public class Response
        {
            public Guid VideoId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var video = await _context.Videos.FindAsync(request.Video.VideoId);

                if (video == null) {
                    video = new Video();
                    _context.Videos.Add(video);
                }

                video.Title = request.Video.Title;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { VideoId = video.VideoId };
            }
        }
    }
}
