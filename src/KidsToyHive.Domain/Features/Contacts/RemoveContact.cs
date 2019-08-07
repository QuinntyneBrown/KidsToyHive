using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Contacts
{
    public class RemoveContact
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ContactId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid ContactId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var contact = await _context.Contacts.FindAsync(request.ContactId);

                _context.Contacts.Remove(contact);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
