using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ContactService.Features.ContactRequests
{
    public class GetContactRequestByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ContactRequestId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ContactRequestId { get; set; }
        }

        public class Response
        {
            public ContactRequestApiModel ContactRequest { get; set; }
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
                    ContactRequest = ContactRequestApiModel.FromContactRequest(await _context.ContactRequests.FindAsync(request.ContactRequestId))
                };
        }
    }
}
