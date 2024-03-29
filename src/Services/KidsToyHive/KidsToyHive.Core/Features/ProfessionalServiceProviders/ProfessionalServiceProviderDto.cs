// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.ProfessionalServiceProviders;

public class ProfessionalServiceProviderDtoValidator : AbstractValidator<ProfessionalServiceProviderDto>
{
    public ProfessionalServiceProviderDtoValidator()
    {
        RuleFor(x => x.ProfessionalServiceProviderId).NotNull();
        RuleFor(x => x.FullName).NotNull();
    }
}
public class ProfessionalServiceProviderDto
{
    public Guid ProfessionalServiceProviderId { get; set; }
    public string FullName { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public int Version { get; set; }
}
public static class ProfessionalServiceProviderExtensions
{
    public static ProfessionalServiceProviderDto ToDto(this ProfessionalServiceProvider professionalServiceProvider)
        => new ProfessionalServiceProviderDto
        {
            ProfessionalServiceProviderId = professionalServiceProvider.ProfessionalServiceProviderId,
            FullName = professionalServiceProvider.FullName,
            Title = professionalServiceProvider.Title,
            ImageUrl = professionalServiceProvider.ImageUrl,
            Version = professionalServiceProvider.Version
        };
}

