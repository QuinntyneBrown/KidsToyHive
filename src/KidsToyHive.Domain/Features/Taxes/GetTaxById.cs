using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Taxes
{
    public class GetTaxById
    {
        public class Request : IRequest<Response> {
            public Guid TaxId { get; set; }
        }

        public class Response
        {
            public TaxDto Tax { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Tax = (await _context.Taxes.FindAsync(request.TaxId)).ToDto()
                };
        }
    }
}
