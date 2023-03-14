using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProfessionalServiceProviders;

public class GetProfessionalServiceProviderById
{
    public class Request : IRequest<Response>
    {
        public Guid ProfessionalServiceProviderId { get; set; }
    }
    public class Response
    {
        public ProfessionalServiceProviderDto ProfessionalServiceProvider { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        public IAppDbContext _context { get; set; }
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                ProfessionalServiceProvider = (await _context.ProfessionalServiceProviders.FindAsync(request.ProfessionalServiceProviderId)).ToDto()
            };
    }
}
