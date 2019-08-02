using FluentValidation;
using KidsToyHive.Api.Filters;
using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
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
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KidsToyHive.Api
{
    public static class Dependencies
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(isOriginAllowed: _ => true)
                .AllowCredentials()));

            services.AddHttpContextAccessor();
            services.AddSingleton<ICommandRegistry, CommandRegistry>();
            services.AddSingleton<ISecurityTokenFactory, SecurityTokenFactory>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ISecurityTokenFactory, SecurityTokenFactory>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailBuilder, EmailBuilder>();
            services.AddTransient<IEmailDeliveryService, EmailDeliveryService>();
            services.AddTransient<IEmailDistributionService, EmailDistributionService>();
            services.AddTransient<IInventoryService, InventoryService>();

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kids Toy Hive Api",
                    Version = "v1",
                    Description = "Kids Toy Hive Api",
                });
                options.CustomSchemaIds(x => x.FullName);
            });

            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

            services.AddMediatR(p =>
            {
                p.AsTransient();

            }, typeof(Authenticate).GetTypeInfo().Assembly);

            services.Scan(
                scan => scan.FromAssemblies(typeof(Authenticate).GetTypeInfo().Assembly)
                    .AddClasses(x => x.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());

            services.Scan(
                scan => scan.FromAssemblies(typeof(Startup).GetTypeInfo().Assembly)
                    .AddClasses(x => x.AssignableTo(typeof(IPipelineBehavior<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            services.ConfigureSwaggerGen(options => {
                options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler
            {
                InboundClaimTypeMap = new Dictionary<string, string>()
            };

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(jwtSecurityTokenHandler);
                    options.TokenValidationParameters = GetTokenValidationParameters(configuration);
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

            services.AddTransient<IAppDbContext, AppDbContext>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options
                .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"], b => b.MigrationsAssembly("KidsToyHive.Api"));
            });
        }
                private static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtKey"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                NameClaimType = JwtRegisteredClaimNames.UniqueName
            };

            return tokenValidationParameters;
        }
    }
}
