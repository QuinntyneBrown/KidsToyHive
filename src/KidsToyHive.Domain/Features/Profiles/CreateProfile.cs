using KidsToyHive.Core.Identity;
using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Profiles;

public class CreateProfileRequest : IRequest<CreateProfileResponse>
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
public class CreateProfileResponse
{
    public Guid ProfileId { get; set; }
}
public class CreateProfileHandler : IRequestHandler<CreateProfileRequest, CreateProfileResponse>
{
    private readonly IAppDbContext _context;
    public IPasswordHasher _passwordHasher { get; set; }
    public CreateProfileHandler(IAppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }
    public async Task<CreateProfileResponse> Handle(CreateProfileRequest request, CancellationToken cancellationToken)
    {
        var profile = new Profile() { Name = request.Name, AvatarUrl = request.AvatarUrl };
        profile.User = new User() { Username = request.Username };
        profile.User.Password = _passwordHasher.HashPassword(profile.User.Salt, request.Password);
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreateProfileResponse() { ProfileId = profile.ProfileId };
    }
}
