using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Roles;

public class RemoveRoleValidator : AbstractValidator<RemoveRoleRequest>
{
    public RemoveRoleValidator()
    {
        RuleFor(request => request.RoleId).NotNull();
    }
}
public class RemoveRoleRequest : IRequest
{
    public Guid RoleId { get; set; }
}
public class RemoveRoleHandler : IRequestHandler<RemoveRoleRequest>
{
    private readonly IAppDbContext _context;
    public RemoveRoleHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FindAsync(request.RoleId);
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
