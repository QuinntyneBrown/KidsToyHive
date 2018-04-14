using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ContactService.Features.Contacts
{
    public class SaveContactCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Contact.ContactId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ContactApiModel Contact { get; set; }
        }

        public class Response
        {			
            public int ContactId { get; set; }
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
                var contact = await _context.Contacts.FindAsync(request.Contact.ContactId);

                if (contact == null) _context.Contacts.Add(contact = new Contact());

                contact.Name = request.Contact.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ContactId = contact.ContactId };
            }
        }
    }
}
