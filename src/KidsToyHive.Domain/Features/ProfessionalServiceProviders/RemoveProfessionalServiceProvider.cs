using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProfessionalServiceProviders
{
    public class RemoveProfessionalServiceProvider
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProfessionalServiceProviderId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid ProfessionalServiceProviderId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var professionalServiceProvider = await _context.ProfessionalServiceProviders.FindAsync(request.ProfessionalServiceProviderId);

                _context.ProfessionalServiceProviders.Remove(professionalServiceProvider);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
