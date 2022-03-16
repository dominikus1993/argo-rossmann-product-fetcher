using System.Text.Json;

using ProductFetcher.Core.Dto;
using ProductFetcher.Core.Services;

namespace ProductFetcher.Infrastructure.Services;

internal class ConsolePromotionNotifier : IPromotionNotifier
{
    public ValueTask Notify(IEnumerable<RossmannProductDto> products, CancellationToken cancellationToken = default)
    {
        foreach (var product in products)
        {
            Console.WriteLine(JsonSerializer.Serialize(product, new JsonSerializerOptions() { WriteIndented = true }));
        }

        return new ValueTask();
    }
}