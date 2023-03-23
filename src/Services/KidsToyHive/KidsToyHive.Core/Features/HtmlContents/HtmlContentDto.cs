// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.HtmlContents;

public class HtmlContentDtoValidator : AbstractValidator<HtmlContentDto>
{
    public HtmlContentDtoValidator()
    {
        RuleFor(x => x.HtmlContentId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class HtmlContentDto
{
    public Guid HtmlContentId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public int Version { get; set; }
}
public static class HtmlContentExtensions
{
    public static HtmlContentDto ToDto(this HtmlContent htmlContent)
        => new HtmlContentDto
        {
            HtmlContentId = htmlContent.HtmlContentId,
            Name = htmlContent.Name,
            Value = htmlContent.Value,
            Version = htmlContent.Version
        };
}

