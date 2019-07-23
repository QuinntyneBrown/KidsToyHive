using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using KidsToyHive.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KidsToyHive.Domain.DataAccess;

namespace KidsToyHive.Domain.Features.Profiles
{
    public class GetProfiles
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ProfileDto> Profiles { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Profiles = await _context.Profiles.Select(x => ProfileDto.FromProfile(x)).ToListAsync()
                };
        }
    }
}
