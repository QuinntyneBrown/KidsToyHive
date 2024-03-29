// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Infrastructure.Data;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace KidsToyHive.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateWebHostBuilder(args).Build();
        ProcessDbCommands(args, host);
        host.Run();
    }
    private static void ProcessDbCommands(string[] args, IWebHost host)
    {
        var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
        using (var scope = services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<KidsToyHiveDbContext>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            if (args.Contains("ci"))
                args = new string[4] { "dropdb", "migratedb", "seeddb", "stop" };
            if (args.Contains("dropdb"))
            {
                context.Database.EnsureDeleted();
            }
            if (args.Contains("migratedb"))
            {
                context.Database.Migrate();
            }
            if (args.Contains("seeddb"))
            {
                context.Database.EnsureCreated();
                SeedData.Seed(context, configuration);
            }
            if (args.Contains("secret"))
            {
                var tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
                tripleDESCryptoServiceProvider.GenerateKey();
                var key = System.Convert.ToBase64String(tripleDESCryptoServiceProvider.Key);
                Console.WriteLine(key);
                Environment.Exit(0);
            }
            if (args.Contains("stop"))
                Environment.Exit(0);
        }
    }
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .UseApplicationInsights()
        .UseSerilog((builderContext, config) =>
        {
            config
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console();
                //.WriteTo.ApplicationInsightsTraces(new TelemetryClient());
        })
        .UseStartup<Startup>();
}

