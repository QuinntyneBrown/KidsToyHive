using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Profiles
{
    public class CreateProfile
    {
        public class Request : IRequest<Response> {

            public string Username { get; set; }
            public string Name { get; set; }
            public string AvatarUrl { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class Response
        {
            public Guid ProfileId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public IPasswordHasher _passwordHasher { get; set; }
            public Handler(IAppDbContext context, IPasswordHasher passwordHasher) {
                _context = context;
                _passwordHasher = passwordHasher;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var profile = new Profile() { Name = request.Name, AvatarUrl = request.AvatarUrl };
                profile.User = new User() { Username = request.Username };
                profile.User.Password = _passwordHasher.HashPassword(profile.User.Salt, request.Password);                                
                _context.Profiles.Add(profile);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProfileId = profile.ProfileId };
            }
        }
    }
}
