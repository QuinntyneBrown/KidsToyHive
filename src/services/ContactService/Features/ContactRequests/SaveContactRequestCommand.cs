using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ContactService.Features.ContactRequests
{
    public class SaveContactRequestCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ContactRequest.ContactRequestId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ContactRequestApiModel ContactRequest { get; set; }
        }

        public class Response
        {			
            public int ContactRequestId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var contactRequest = await _context.ContactRequests.FindAsync(request.ContactRequest.ContactRequestId);

                if (contactRequest == null) _context.ContactRequests.Add(contactRequest = new ContactRequest());

                contactRequest.Name = request.ContactRequest.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ContactRequestId = contactRequest.ContactRequestId };
            }
        }
    }
}
