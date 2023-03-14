using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using KidsToyHive.Domain;

namespace KidsToyHive.Domain.Features.Profiles;

public class GetProfileByUsernameRequest : IRequest<GetProfileByUsernameResponse>
{
    public string Username { get; set; }
}
public class GetProfileByUsernameResponse
{
    public ProfileDto Profile { get; set; }
}
public class GetProfileByUsernameHandler : IRequestHandler<GetProfileByUsernameRequest, GetProfileByUsernameResponse>
{
    private readonly IAppDbContext _context;
    public GetProfileByUsernameHandler(IAppDbContext context) => _context = context;
    public async Task<GetProfileByUsernameResponse> Handle(GetProfileByUsernameRequest request, CancellationToken cancellationToken)
        => new GetProfileByUsernameResponse()
        {
            Profile = ProfileDto.FromProfile(await _context.Profiles.SingleAsync(x => x.User.Username == request.Username))
        };
}
