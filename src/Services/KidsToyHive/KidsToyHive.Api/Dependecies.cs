// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using KidsToyHive.Api.Filters;
using KidsToyHive.Api.HealthChecks;
using KidsToyHive.Core.Identity;
using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Services;
using KidsToyHive.Domain.Common;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.Users;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KidsToyHive.Domain;
using KidsToyHive.Core.Extensions;
using FluentValidation;

namespace KidsToyHive.Api;

public static class Dependencies
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder => builder
            .WithOrigins(configuration["Cors:Origins"])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(isOriginAllowed: _ => true)
            .AllowCredentials()));
        services.AddHttpContextAccessor();
        services.AddSingleton<ICommandRegistry, CommandRegistry>();
        services.AddSingleton<ISecurityTokenFactory, SecurityTokenFactory>();
        services.AddSingleton<ICache, InMemoryCache>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ISecurityTokenFactory, SecurityTokenFactory>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IEmailBuilder, EmailBuilder>();
        services.AddTransient<IEmailDistributionService, EmailDistributionService>();
        services.AddTransient<IInventoryService, InventoryService>();
        services.AddTransient<IPaymentProcessor, PaymentProcessor>();
        services.AddTransient<IEmailDeliveryService, EmailDeliveryService>();
        services.AddHealthChecks()
        .AddCheck<SystemMemoryHealthcheck>("Memory");
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Kids Toy Hive Api",
                Version = "v1",
                Description = "Kids Toy Hive Api",
            });
            options.CustomSchemaIds(x => x.FullName);
        });
        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<AuthenticateRequest>());

        services.ConfigureSwaggerGen(options =>
        {
            options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
        });

        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Request.Query.TryGetValue("access_token", out StringValues token);
                        if (!string.IsNullOrEmpty(token))
                            context.Token = token;
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddControllers(x =>
        {
            x.Filters.Add(new AuthorizeFilter());
        })
        .AddNewtonsoftJson()
        .SetCompatibilityVersion(CompatibilityVersion.Latest);
        services.AddTransient<IKidsToyHiveDbContext, KidsToyHiveDbContext>();
        services.AddDbContext<KidsToyHiveDbContext>(options =>
        {
            options
            .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"], b => b.MigrationsAssembly("KidsToyHive.Api"));
        });
    }
}

