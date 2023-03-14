using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KidsToyHive.Domain.Common;

namespace KidsToyHive.Domain.Features.Contacts;

public class UpsertContactValidator : AbstractValidator<UpsertContactRequest>
{
    public UpsertContactValidator()
    {
        RuleFor(request => request.Contact).NotNull();
        RuleFor(request => request.Contact).SetValidator(new ContactDtoValidator());
    }
}
[AllowAnonymous]
public class UpsertContactRequest : Command<UpsertContactResponse>
{
    public ContactDto Contact { get; set; }
}
public class UpsertContactResponse
{
    public Guid ContactId { get; set; }
    public int Version { get; set; }
}
public class UpsertContactHandler : IRequestHandler<UpsertContactRequest, UpsertContactResponse>
{
    public IAppDbContext _context { get; set; }
    public UpsertContactHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertContactResponse> Handle(UpsertContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts.FindAsync(request.Contact.ContactId);
        if (contact == null)
        {
            contact = new Contact();
            _context.Contacts.Add(contact);
        }
        contact.FullName = request.Contact.FullName;
        contact.PhoneNumber = request.Contact.PhoneNumber;
        contact.Email = request.Contact.Email;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertContactResponse()
        {
            ContactId = contact.ContactId,
            Version = contact.Version
        };
    }
}
