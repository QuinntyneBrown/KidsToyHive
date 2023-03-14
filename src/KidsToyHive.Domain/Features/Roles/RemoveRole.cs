using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Roles;

public class RemoveRole
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.RoleId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid RoleId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(request.RoleId);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
