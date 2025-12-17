// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;

namespace KidsToyHive.Core.Features.Cards;

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

