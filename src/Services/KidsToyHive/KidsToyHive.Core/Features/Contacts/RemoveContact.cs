// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Contacts;

public class RemoveContactValidator : AbstractValidator<RemoveContactRequest>
{
    public RemoveContactValidator()
    {
        RuleFor(request => request.ContactId).NotNull();
    }
}
public class RemoveContactRequest : IRequest
{
    public Guid ContactId { get; set; }
}
public class RemoveContactHandler : IRequestHandler<RemoveContactRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveContactHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts.FindAsync(request.ContactId);
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

