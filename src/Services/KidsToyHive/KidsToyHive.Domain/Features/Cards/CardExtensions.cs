using KidsToyHive.Domain.Models;

namespace KidsToyHive.Domain.Features.Cards;

public static class CardExtensions
{
    public static CardDto ToDto(this Card card)
        => new CardDto
        {
            CardId = card.CardId,
            Name = card.Name,
            Version = card.Version
        };
}
