using FluentValidation;

namespace KidsToyHive.Domain.Features.Cards;

public class CardDtoValidator : AbstractValidator<CardDto>
{
    public CardDtoValidator()
    {
        RuleFor(x => x.CardId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
