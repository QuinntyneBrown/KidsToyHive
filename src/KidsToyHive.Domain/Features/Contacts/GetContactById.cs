using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Contacts;

public class GetContactById
{
    public class Request : IRequest<Response>
    {
        public Guid ContactId { get; set; }
    }
    public class Response
    {
        public ContactDto Contact { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        public IAppDbContext _context { get; set; }
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                Contact = (await _context.Contacts.FindAsync(request.ContactId)).ToDto()
            };
    }
}
