using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Users
{
    public class Authenticate
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Username).NotEqual(default(string));
                RuleFor(request => request.Password).NotEqual(default(string));
            }            
        }

        public class Request : IRequest<Response>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public string AccessToken { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
            private readonly ISecurityTokenFactory _securityTokenFactory;

            public Handler(IAppDbContext context, ISecurityTokenFactory securityTokenFactory, IPasswordHasher passwordHasher)
            {
                _context = context;
                _securityTokenFactory = securityTokenFactory;
                _passwordHasher = passwordHasher;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .SingleOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower());

                if (user == null)
                    throw new Exception();

                if (!ValidateUser(user, _passwordHasher.HashPassword(user.Salt, request.Password)))
                    throw new Exception();

                var profiles = _context.Profiles
                    .Include(x => x.User)
                    .Where(x => x.User.Username == request.Username)
                    .ToList();

                var claims = new List<Claim>();

                foreach (var profile in profiles) {
                    claims.Add(new Claim("ProfileId", $"{profile.ProfileId}"));
                    if (profile.Type == ProfileType.Customer)
                        claims.Add(new Claim(ClaimTypes.Role, nameof(ProfileType.Customer)));

                    if (profile.Type == ProfileType.Admin)
                        claims.Add(new Claim(ClaimTypes.Role, nameof(ProfileType.Admin)));

                    if (profile.Type == ProfileType.Driver)
                        claims.Add(new Claim(ClaimTypes.Role, nameof(ProfileType.Driver)));
                }

                return new Response()
                {
                    AccessToken = _securityTokenFactory.Create(request.Username, claims),
                    UserId = user.UserId
                };
            }

            public bool ValidateUser(User user, string transformedPassword)
            {
                if (user == null || transformedPassword == null)
                    return false;

                return user.Password == transformedPassword;
            }
        }
    }
}
