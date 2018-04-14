using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ContactService.Features.Contacts
{
    public class GetContactByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ContactId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ContactId { get; set; }
        }

        public class Response
        {
            public ContactApiModel Contact { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Contact = ContactApiModel.FromContact(await _context.Contacts.FindAsync(request.ContactId))
                };
        }
    }
}
