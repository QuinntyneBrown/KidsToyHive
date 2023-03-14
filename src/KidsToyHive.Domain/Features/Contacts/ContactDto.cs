using KidsToyHive.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KidsToyHive.Domain.Features.Contacts;

public class ContactDtoValidator : AbstractValidator<ContactDto>
{
    public ContactDtoValidator()
    {
        RuleFor(x => x.ContactId).NotNull();
    }
}
public class ContactDto
{
    public Guid ContactId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<ContactMessageDto> ContactMessages { get; set; }
        = new HashSet<ContactMessageDto>();
    public int Version { get; set; }
}
public static class ContactExtensions
{
    public static ContactDto ToDto(this Contact contact)
        => new ContactDto
        {
            ContactId = contact.ContactId,
            FullName = contact.FullName,
            PhoneNumber = contact.PhoneNumber,
            Version = contact.Version,
            ContactMessages = contact.ContactMessages.Select(x => x.ToDto()).ToList()
        };
}
