// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KidsToyHive.Core.Common;

namespace KidsToyHive.Core.Features.Contacts;

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
    public IKidsToyHiveDbContext _context { get; set; }
    public UpsertContactHandler(IKidsToyHiveDbContext context) => _context = context;
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

