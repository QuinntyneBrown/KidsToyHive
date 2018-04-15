using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;
using System;

namespace TenantService.Features.Tenants
{
    public class RemoveTenantCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Tenant.TenantId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Tenant Tenant { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Tenants.Remove(await _context.Tenants.FindAsync(request.Tenant.TenantId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
