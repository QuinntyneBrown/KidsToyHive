// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Cards;

public class RemoveCardValidator : AbstractValidator<RemoveCardRequest>
{
    public RemoveCardValidator()
    {
        RuleFor(request => request.CardId).NotNull();
    }
}
public class RemoveCardRequest : IRequest
{
    public Guid CardId { get; set; }
}
public class RemoveCardHandler : IRequestHandler<RemoveCardRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveCardHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveCardRequest request, CancellationToken cancellationToken)
    {
        var card = await _context.Cards.FindAsync(request.CardId);
        _context.Cards.Remove(card);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

