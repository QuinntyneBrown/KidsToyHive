using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.CardLayouts
{
    public class CardLayoutDtoValidator: AbstractValidator<CardLayoutDto>
    {
        public CardLayoutDtoValidator()
        {
            RuleFor(x => x.CardLayoutId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class CardLayoutDto
    {        
        public Guid CardLayoutId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class CardLayoutExtensions
    {        
        public static CardLayoutDto ToDto(this CardLayout cardLayout)
            => new CardLayoutDto
            {
                CardLayoutId = cardLayout.CardLayoutId,
                Name = cardLayout.Name,
                Version = cardLayout.Version
            };
    }
}
