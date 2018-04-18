using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Infrastructure.Filters;
using Infrastructure.Configuration;
using Infrastructure.Extensions;
using Infrastructure.Services;
using KidsToyHive.SPA.Clients;
using System;

namespace KidsToyHive.SPA
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

            services.AddHttpClient<ProductsClient>(client => client.BaseAddress = new Uri(Configuration["ClusterSettings:ProductsServiceBaseUrl"]));

            services.AddTransient<IEncryptionService, EncryptionService>();
            ConfigureDataStore(services);
            services.AddCustomSwagger();
            services.AddCustomCache();
            services.AddCustomMvc();
            services.AddSignalR();
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist/ClientApp");
        }

        public virtual void ConfigureDataStore(IServiceCollection services)
            => services.AddDataStore(Configuration["Data:DefaultConnection:ConnectionString"]);

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
                app.UseHsts();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    //spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
