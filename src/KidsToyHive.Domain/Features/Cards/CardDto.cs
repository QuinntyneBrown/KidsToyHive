using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Cards
{
    public class CardDtoValidator: AbstractValidator<CardDto>
    {
        public CardDtoValidator()
        {
            RuleFor(x => x.CardId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class CardDto
    {        
        public Guid CardId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

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
}
