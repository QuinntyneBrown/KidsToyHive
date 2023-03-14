using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Core.Interfaces;
using FluentValidation;
using KidsToyHive.Domain.DataAccess;

namespace KidsToyHive.Domain.Features.Profiles;

public class GetProfileById
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
    public class Response
    {
        public ProfileDto Profile { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;

        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                Profile = ProfileDto.FromProfile(await _context.Profiles.FindAsync(request.ProfileId))
            };
    }
}
