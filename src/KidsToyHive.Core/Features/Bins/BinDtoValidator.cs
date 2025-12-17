// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;

namespace KidsToyHive.Core.Features.Bins;

public class BinDtoValidator : AbstractValidator<BinDto>
{
    public BinDtoValidator()
    {
        RuleFor(x => x.BinId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}

