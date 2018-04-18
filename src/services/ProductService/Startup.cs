using Infrastructure.Behaviours;
using Infrastructure.Extensions;
using Infrastructure.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ProductService
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomConfiguration(Configuration);
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            ConfigureDataStore(services);
            services.AddCustomSwagger();
            services.AddMediatR(typeof(Startup));
            services.AddCustomCache();
            services.AddCustomMvc();
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        public virtual void ConfigureDataStore(IServiceCollection services)
            => services.AddDataStore(Configuration["Data:DefaultConnection:ConnectionString"]);

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            ConfigureTenantIdAndUsernameResolution(app);
            app.UseMvc();
            app.UseCustomSwagger();
        }

        public virtual void ConfigureTenantIdAndUsernameResolution(IApplicationBuilder app)
            => app.UseMiddleware<TenantIdAndUsernameMiddleware>();
    }
}
