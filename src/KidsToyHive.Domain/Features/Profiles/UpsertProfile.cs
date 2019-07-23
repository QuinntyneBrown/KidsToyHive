using FluentValidation;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Profiles
{
    public class UpsertProfile
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Profile.ProfileId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ProfileDto Profile { get; set; }
        }

        public class Response
        {            
            public Guid ProfileId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var profile = await _context.Profiles.FindAsync(request.Profile.ProfileId);

                if (profile == null) _context.Profiles.Add(profile = new Profile());

                profile.Name = request.Profile.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProfileId = profile.ProfileId };
            }
        }
    }
}
