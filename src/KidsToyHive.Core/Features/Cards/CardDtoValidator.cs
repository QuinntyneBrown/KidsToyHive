// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;

namespace KidsToyHive.Core.Features.Cards;

public class CardDtoValidator : AbstractValidator<CardDto>
{
    public CardDtoValidator()
    {
        RuleFor(x => x.CardId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}

