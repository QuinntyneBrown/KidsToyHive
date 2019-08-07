using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProfessionalServiceProviders
{
    public class UpsertProfessionalServiceProvider
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ProfessionalServiceProvider).NotNull();
                RuleFor(request => request.ProfessionalServiceProvider).SetValidator(new ProfessionalServiceProviderDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public ProfessionalServiceProviderDto ProfessionalServiceProvider { get; set; }
        }

        public class Response
        {
            public Guid ProfessionalServiceProviderId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var professionalServiceProvider = await _context.ProfessionalServiceProviders.FindAsync(request.ProfessionalServiceProvider.ProfessionalServiceProviderId);

                if (professionalServiceProvider == null) {
                    professionalServiceProvider = new ProfessionalServiceProvider();
                    _context.ProfessionalServiceProviders.Add(professionalServiceProvider);
                }

                professionalServiceProvider.FullName = request.ProfessionalServiceProvider.FullName;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProfessionalServiceProviderId = professionalServiceProvider.ProfessionalServiceProviderId };
            }
        }
    }
}
