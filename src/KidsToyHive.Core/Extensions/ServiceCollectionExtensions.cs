// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KidsToyHive.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services, Assembly assembly)
    {
        services.Scan(
            scan => scan.FromAssemblies(assembly)
                .AddClasses(x => x.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
        );
        return services;
    }
}

