using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Roles
{
    public class GetRoleById
    {
        public class Request : IRequest<Response> {
            public Guid RoleId { get; set; }
        }

        public class Response
        {
            public RoleDto Role { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Role = (await _context.Roles.FindAsync(request.RoleId)).ToDto()
                };
        }
    }
}
