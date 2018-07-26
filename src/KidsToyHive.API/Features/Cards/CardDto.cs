using KidsToyHive.Core.Models;
using System;

namespace KidsToyHive.API.Features.Cards
{
    public class CardDto
    {        
        public Guid CardId { get; set; }
        public string Name { get; set; }

        public static CardDto FromCard(Card card)
            => new CardDto
            {
                CardId = card.CardId,
                Name = card.Name
            };
    }
}
