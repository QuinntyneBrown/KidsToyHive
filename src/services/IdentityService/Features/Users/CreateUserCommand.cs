using FluentValidation;
using Infrastructure.Data;
using Core.Entities;
using Infrastructure.Services;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace IdentityService
{
    public class CreateUserCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Username).NotEmpty().WithMessage("Email address is required")
                    .EmailAddress().WithMessage("A valid email is required");

                RuleFor(x => x.Password).Must(password => PasswordValidatorRule(password))
                    .WithMessage("Sorry password didn't satisfy the custom logic");
            }

            private bool PasswordValidatorRule(string password)
                => password.Length > 4;
        }

        public class Request : IRequest<Response>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public int UserId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public IEncryptionService _encryptionService { get; set; }
            public Handler(IAppDbContext context, IEncryptionService encryptionService)
            {
                _context = context;
                _encryptionService = encryptionService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = new User
                    {
                        Username = request.Username,
                        Password = _encryptionService.TransformPassword(request.Password)
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync(cancellationToken);

                    return new Response() { UserId = user.UserId };
                }catch(Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
