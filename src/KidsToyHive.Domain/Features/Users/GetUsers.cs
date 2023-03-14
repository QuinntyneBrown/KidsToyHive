using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Users;

public class GetUsersRequest : IRequest<GetUsersResponse> { }
public class GetUsersResponse
{
    public IEnumerable<UserDto> Users { get; set; }
}
public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IAppDbContext _context;
    public GetUsersHandler(IAppDbContext context) => _context = context;
    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        => new GetUsersResponse()
        {
            Users = await _context.Users.Select(x => x.ToDto()).ToArrayAsync()
        };
}
