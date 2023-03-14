using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using KidsToyHive.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KidsToyHive.Core.Identity;
using System;
using KidsToyHive.Domain.DataAccess;

namespace KidsToyHive.Domain.Features.Users;

public class ChangePasswordRequest : IRequest<ChangePasswordResponse>
{
    public Guid ProfileId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}


public class ChangePasswordResponse { }


public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
{
    private readonly IAppDbContext _context;
    public IPasswordHasher _passwordHasher { get; set; }
    public ChangePasswordHandler(IAppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }
    public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Profiles
            .Include(x => x.User)
            .Where(x => x.ProfileId == request.ProfileId)
            .Select(x => x.User)
            .SingleAsync();

        if (user.Password != _passwordHasher.HashPassword(user.Salt, request.OldPassword))
            throw new Exception();

        user.Password = _passwordHasher.HashPassword(user.Salt, request.NewPassword);

        await _context.SaveChangesAsync(cancellationToken);

        return new ChangePasswordResponse();
    }
}
