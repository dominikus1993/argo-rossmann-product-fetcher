using System.Threading.Tasks;

using Xunit;
using ProductFetcher.Infrastructure.Repositories;
using System.Text.Json;
using System.Linq;


namespace ProductFetcher.Infrastructure.Tests.Repositories;

public class FileProductsReaderTests
{
    [Fact]
    public async Task TestReadingFile()
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
        var reader = new FileProductsReader(new FileProductsReaderConfig("./", "products.json"), options);

        var subject = await reader.ReadProducts().ToListAsync();
        Assert.NotEmpty(subject);
    }
}