using System.Text.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ProductFetcher.Core.Repositories;
using ProductFetcher.Core.Services;
using ProductFetcher.Infrastructure.Api;
using ProductFetcher.Infrastructure.Repositories;
using ProductFetcher.Infrastructure.Services;

using Refit;

namespace ProductFetcher.Infrastructure.Extensions;

public static class IocExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
        var settings = new RefitSettings()
        {
            ContentSerializer = new SystemTextJsonContentSerializer(options)
        };
        services.AddRefitClient<IProductsApi>(settings).ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(configuration.GetConnectionString("ProductsApi"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        services.AddTransient<IRossmannProductsService, HttpRossmannProductsService>();
        services.AddSingleton<JsonSerializerOptions>(options);
        services.AddSingleton(new FileProductsWriterConfig(configuration["WriteSink:Path"], configuration["WriteSink:FileName"]));
        services.AddTransient<IProductsWriter, FileProductsWriter>();
        return services;
    }

        public static IServiceCollection AddNotifierInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
        services.AddSingleton<JsonSerializerOptions>(options);
        services.AddSingleton(new FileProductsReaderConfig(configuration["Source:Path"], configuration["Source:FileName"]));
        services.AddTransient<IProductsReader, FileProductsReader>();
        services.AddTransient<IPromotionNotifier, ConsolePromotionNotifier>();
        return services;
    }
}