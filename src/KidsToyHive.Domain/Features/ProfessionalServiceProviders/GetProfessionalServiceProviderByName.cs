using FluentValidation;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProfessionalServiceProviders
{
    public class GetProfessionalServiceProviderByName
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response> {
            public string FullName { get; set; }
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
                    ProfessionalServiceProvider = (await _context.ProfessionalServiceProviders.SingleAsync(x => x.FullName == request.FullName)).ToDto()
                };
        }
    }
}
