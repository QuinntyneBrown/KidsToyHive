using Infrastructure.Extensions;
using Infrastructure.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ContactService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomConfiguration(Configuration);
            services.AddSecurity(Configuration);
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            ConfigureDataStore(services);
            services.AddCustomSwagger();
            services.AddMediatR(typeof(Startup));
            services.AddCustomCache();
            services.AddCustomMvc();
        }

        public virtual void ConfigureDataStore(IServiceCollection services)
        {
            services.AddDataStore(Configuration["Data:DefaultConnection:ConnectionString"]);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHsts();
            app.UseAuthentication();
            app.UseMiddleware<TenantIdAndUsernameMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            ConfigureDataContext(app);
            app.UseMvc();
            app.UseCustomSwagger();
        }

        public virtual void ConfigureDataContext(IApplicationBuilder app)
        {
            app.UseMiddleware<TenantIdAndUsernameMiddleware>();
        }
    }
}
