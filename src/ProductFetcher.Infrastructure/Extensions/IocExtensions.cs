using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ProductFetcher.Core.Services;
using ProductFetcher.Infrastructure.Api;
using ProductFetcher.Infrastructure.Services;

using Refit;

namespace ProductFetcher.Infrastructure.Extensions;

public static class IocExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRefitClient<IProductsApi>().ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(configuration.GetConnectionString("ProductsApi"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        services.AddTransient<IRossmannProductsService, HttpRossmannProductsService>();
        return services;
    }
}