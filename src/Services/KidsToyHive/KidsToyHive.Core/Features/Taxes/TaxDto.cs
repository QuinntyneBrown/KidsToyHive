// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.Taxes;

public class TaxDtoValidator : AbstractValidator<TaxDto>
{
    public TaxDtoValidator()
    {
        RuleFor(x => x.TaxId).NotNull();
        RuleFor(x => x.Rate).NotNull();
    }
}
public class TaxDto
{
    public Guid TaxId { get; set; }
    public double Rate { get; set; }
    public int Version { get; set; }
}
public static class TaxExtensions
{
    public static TaxDto ToDto(this Tax tax)
        => new TaxDto
        {
            TaxId = tax.TaxId,
            Rate = tax.Rate,
            Version = tax.Version
        };
}

