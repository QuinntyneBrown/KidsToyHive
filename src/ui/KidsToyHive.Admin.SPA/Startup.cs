using Infrastructure.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KidsToyHive.Admin.SPA
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
            services.AddTransient<IEncryptionService, EncryptionService>();
            ConfigureDataStore(services);
            services.AddCustomSwagger();
            services.AddCustomCache();
            services.AddCustomMvc();
            
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist");
        }

        public virtual void ConfigureDataStore(IServiceCollection services)
            => services.AddDataStore(Configuration["Data:DefaultConnection:ConnectionString"]);

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (!env.IsDevelopment())
                app.UseHsts();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseMvc();
            app.UseCustomSwagger();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            
            app.UseSpa(spa =>
            {                
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
