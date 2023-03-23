using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Contacts;

public class ContactMessageDtoValidator : AbstractValidator<ContactMessageDto>
{
    public ContactMessageDtoValidator()
    {
        RuleFor(x => x.ContactMessageId).NotNull();
        RuleFor(x => x.Value).NotNull();
    }
}
public class ContactMessageDto
{
    public Guid ContactMessageId { get; set; }
    public string Value { get; set; }
    public int Version { get; set; }
}
public static class ContactMessageExtensions
{
    public static ContactMessageDto ToDto(this ContactMessage contactMessage)
        => new ContactMessageDto
        {
            ContactMessageId = contactMessage.ContactMessageId,
            Value = contactMessage.Value,
            Version = contactMessage.Version
        };
}
