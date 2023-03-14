using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Users;

public class UpsertUserValidator : AbstractValidator<UpsertUserRequest>
{
    public UpsertUserValidator()
    {
        RuleFor(request => request.User).NotNull();
        RuleFor(request => request.User).SetValidator(new UserDtoValidator());
    }
}
public class UpsertUserRequest : IRequest<UpsertUserResponse>
{
    public UserDto User { get; set; }
}
public class UpsertUserResponse
{
    public Guid UserId { get; set; }
}
public class UpsertUserHandler : IRequestHandler<UpsertUserRequest, UpsertUserResponse>
{
    private readonly IAppDbContext _context;
    public UpsertUserHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertUserResponse> Handle(UpsertUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.User.UserId);
        if (user == null)
        {
            user = new User();
            _context.Users.Add(user);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertUserResponse() { UserId = user.UserId };
    }
}
