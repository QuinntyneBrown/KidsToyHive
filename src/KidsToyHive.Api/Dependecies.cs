using KidsToyHive.Api.Behaviours;
using KidsToyHive.Api.Filters;
using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
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
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            services.AddSingleton<ITokenProvider, TokenProvider>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ISecurityTokenFactory, SecurityTokenFactory>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthenticatedRequestBehavior<,>));

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
            services.AddDbContext<AppDbContext>(
                options => options.UseCosmos(
                    Environment.GetEnvironmentVariable("CosmosDb:EndpointUrl"),
                    Environment.GetEnvironmentVariable("CosmosDb:PrivateKey"),
                    Environment.GetEnvironmentVariable("CosmosDb:DbName")));

            services.AddTransient<IAppDbContext, AppDbContext>();
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
