using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Users;

public class GetUserByIdRequest : IRequest<GetUserByIdResponse>
{
    public Guid UserId { get; set; }
}
public class GetUserByIdResponse
{
    public UserDto User { get; set; }
}
public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetUserByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        => new GetUserByIdResponse()
        {
            User = (await _context.Users.FindAsync(request.UserId)).ToDto()
        };
}
