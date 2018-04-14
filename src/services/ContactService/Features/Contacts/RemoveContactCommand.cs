using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ContactService.Features.Contacts
{
    public class RemoveContactCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Contact.ContactId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Contact Contact { get; set; }
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
                _context.Contacts.Remove(await _context.Contacts.FindAsync(request.Contact.ContactId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
