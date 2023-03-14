using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Users;

public class RemoveUser
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.UserId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid UserId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
