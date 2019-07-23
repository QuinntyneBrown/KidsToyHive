using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Domain.Models;
using KidsToyHive.Core.Interfaces;
using KidsToyHive.Domain.DataAccess;

namespace KidsToyHive.Domain.Features.Profiles
{
    public class RemoveProfile
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProfileId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response>
        {
            public int ProfileId { get; set; }
        }

        public class Response { }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Profiles.Remove(await _context.Profiles.FindAsync(request.ProfileId));
                await _context.SaveChangesAsync(cancellationToken);
                return new Response()
                {

                };
            }

        }
    }
}
