using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using KidsToyHive.Domain.DataAccess;

namespace KidsToyHive.Domain.Features.Profiles
{
    public class GetProfileByUsername
    {
        public class Request : IRequest<Response> {
            public string Username { get; set; }
        }

        public class Response
        {
            public ProfileDto Profile { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) 
                => new Response()
                {
                    Profile = ProfileDto.FromProfile(await _context.Profiles.SingleAsync(x => x.User.Username == request.Username))
                };
        }
    }
}
