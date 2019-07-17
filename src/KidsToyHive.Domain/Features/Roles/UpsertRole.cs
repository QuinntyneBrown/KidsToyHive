using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Roles
{
    public class UpsertRole
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Role).NotNull();
                RuleFor(request => request.Role).SetValidator(new RoleDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public RoleDto Role { get; set; }
        }

        public class Response
        {
            public Guid RoleId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var role = await _context.Roles.FindAsync(request.Role.RoleId);

                if (role == null) {
                    role = new Role();
                    _context.Roles.Add(role);
                }

                role.Name = request.Role.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { RoleId = role.RoleId };
            }
        }
    }
}
