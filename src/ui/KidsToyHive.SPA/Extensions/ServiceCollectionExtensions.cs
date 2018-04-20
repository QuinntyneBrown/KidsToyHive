using KidsToyHive.SPA.Clients;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace KidsToyHive.SPA.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductServiceClients(this IServiceCollection services, string baseUrl)
        {
            services.AddHttpClient<BrandsClient>(client => client.BaseAddress = new Uri(baseUrl));
            services.AddHttpClient<ProductsClient>(client => client.BaseAddress = new Uri(baseUrl));
            services.AddHttpClient<ProductImagesClient>(client => client.BaseAddress = new Uri(baseUrl));
            services.AddHttpClient<ProductCategoriesClient>(client => client.BaseAddress = new Uri(baseUrl));
            return services;
        }
    }
}
