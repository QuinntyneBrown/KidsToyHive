using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ContactService.Features.ContactRequests
{
    public class RemoveContactRequestCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ContactRequest.ContactRequestId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public ContactRequest ContactRequest { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.ContactRequests.Remove(await _context.ContactRequests.FindAsync(request.ContactRequest.ContactRequestId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
