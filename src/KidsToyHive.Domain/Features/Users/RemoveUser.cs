using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Users;

public class RemoveUserValidator : AbstractValidator<RemoveUserRequest>
{
    public RemoveUserValidator()
    {
        RuleFor(request => request.UserId).NotNull();
    }
}
public class RemoveUserRequest : IRequest
{
    public Guid UserId { get; set; }
}
public class RemoveUserHandler : IRequestHandler<RemoveUserRequest>
{
    private readonly IAppDbContext _context;
    public RemoveUserHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.UserId);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
