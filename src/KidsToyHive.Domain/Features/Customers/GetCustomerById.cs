using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Customers
{
    public class GetCustomerById
    {
        public class Request : IRequest<Response> {
            public Guid CustomerId { get; set; }
        }

        public class Response
        {
            public CustomerDto Customer { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Customer = (await _context.Customers
                    .Include(x => x.Address)
                    .Include(x => x.CustomerLocations)
                    .ThenInclude(x => x.Location)
                    .ThenInclude(x => x.Adddress)
                    .SingleAsync(x => x.CustomerId == request.CustomerId)                    
                    ).ToDto()
                };
        }
    }
}
