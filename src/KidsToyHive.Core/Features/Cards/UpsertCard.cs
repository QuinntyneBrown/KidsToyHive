// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Cards;

public class UpsertCardValidator : AbstractValidator<UpsertCardRequest>
{
    public UpsertCardValidator()
    {
        RuleFor(request => request.Card).NotNull();
        RuleFor(request => request.Card).SetValidator(new CardDtoValidator());
    }
}
public class UpsertCardRequest : IRequest<UpsertCardResponse>
{
    public CardDto Card { get; set; }
}
public class UpsertCardResponse
{
    public Guid CardId { get; set; }
}
public class UpsertCardHandler : IRequestHandler<UpsertCardRequest, UpsertCardResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertCardHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertCardResponse> Handle(UpsertCardRequest request, CancellationToken cancellationToken)
    {
        var card = await _context.Cards.FindAsync(request.Card.CardId);
        if (card == null)
        {
            card = new Card();
            _context.Cards.Add(card);
        }
        card.Name = request.Card.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertCardResponse() { CardId = card.CardId };
    }
}

