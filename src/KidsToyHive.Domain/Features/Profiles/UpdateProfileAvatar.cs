using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Profiles
{
    public class UpdateProfileAvatar
    {
        public class Request : IRequest<Response> {

            public int ProfileId { get; set; }
            public string AvatarUrl { get; set; }
        }

        public class Response
        {
            public Guid ProfileId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var profile = _context.Profiles.Find(request.ProfileId);
                profile.AvatarUrl = request.AvatarUrl;
                await _context.SaveChangesAsync(cancellationToken);
                return new Response() {
                    ProfileId = profile.ProfileId
                };
            }
        }
    }
}
