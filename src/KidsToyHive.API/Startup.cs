using FluentValidation.AspNetCore;
using KidsToyHive.Core;
using KidsToyHive.Core.Behaviours;
using KidsToyHive.Core.Extensions;
using KidsToyHive.Core.Identity;
using KidsToyHive.Core.Interfaces;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KidsToyHive.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEventStore, EventStore>();
            services.AddHttpContextAccessor();

            services.AddCustomMvc()
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });

            services
                .AddCustomSecurity(Configuration)
                .AddCustomSignalR()
                .AddCustomSwagger()
                .AddDataStore(Configuration["Data:DefaultConnection:ConnectionString"],Configuration.GetValue<bool>("isTest"))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddMediatR(typeof(Startup).Assembly);
        }

        public void Configure(IApplicationBuilder app)
        {
            if(Configuration.GetValue<bool>("isTest"))
                app.UseMiddleware<AutoAuthenticationMiddleware>();
                    
            app.UseAuthentication()            
                .UseCors(CorsDefaults.Policy)            
                .UseMvc()
                .UseSignalR(routes => routes.MapHub<IntegrationEventsHub>("/hub"))
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "KidsToyHive API");
                    options.RoutePrefix = string.Empty;
                });
        }        
    }


}
