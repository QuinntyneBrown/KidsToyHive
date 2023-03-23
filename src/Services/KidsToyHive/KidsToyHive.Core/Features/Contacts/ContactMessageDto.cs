// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.Contacts;

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

