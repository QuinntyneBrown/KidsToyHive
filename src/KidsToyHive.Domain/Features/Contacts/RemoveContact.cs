using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Contacts;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(request => request.ContactId).NotNull();
    }
}
public class RemoveContactRequest : IRequest
{
    public Guid ContactId { get; set; }
}
public class RemoveContactHandler : IRequestHandler<Request>
{
    private readonly IAppDbContext _context;
    public RemoveContactHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts.FindAsync(request.ContactId);
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
