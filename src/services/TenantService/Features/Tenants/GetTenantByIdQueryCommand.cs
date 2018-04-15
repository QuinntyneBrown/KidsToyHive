using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace TenantService.Features.Tenants
{
    public class GetTenantByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.TenantId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int TenantId { get; set; }
        }

        public class Response
        {
            public TenantApiModel Tenant { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Tenant = TenantApiModel.FromTenant(await _context.Tenants.FindAsync(request.TenantId))
                };
        }
    }
}
