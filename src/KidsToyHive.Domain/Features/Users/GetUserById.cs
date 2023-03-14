using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Users;

public class GetUserById
{
    public class Request : IRequest<Response>
    {
        public Guid UserId { get; set; }
    }
    public class Response
    {
        public UserDto User { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                User = (await _context.Users.FindAsync(request.UserId)).ToDto()
            };
    }
}
