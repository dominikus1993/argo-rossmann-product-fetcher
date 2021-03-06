using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

using ProductFetcher.Core.Dto;
using ProductFetcher.Core.Repositories;

[assembly: InternalsVisibleTo("ProductFetcher.Infrastructure.Tests")]
namespace ProductFetcher.Infrastructure.Repositories;

internal record FileProductsWriterConfig(string Path, string FileName);

internal class FileProductsWriter : IProductsWriter
{
    private readonly FileProductsWriterConfig _config;
    private readonly JsonSerializerOptions _jsonOptions;

    public FileProductsWriter(FileProductsWriterConfig config, JsonSerializerOptions jsonOptions)
    {
        _config = config;
        _jsonOptions = jsonOptions;
    }

    public async Task WriteProducts(IEnumerable<RossmannProductDto> products, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(products, _jsonOptions);
        var path = System.IO.Path.Combine(_config.Path, _config.FileName);
        await File.WriteAllTextAsync(path, json, cancellationToken);
    }
}

internal record FileProductsReaderConfig(string Path, string FileName);

internal class FileProductsReader : IProductsReader
{
    private readonly FileProductsReaderConfig _config;
    private readonly JsonSerializerOptions _jsonOptions;
    public FileProductsReader(FileProductsReaderConfig config, JsonSerializerOptions jsonOptions)
    {
        _config = config;
        _jsonOptions = jsonOptions;
    }

    public async IAsyncEnumerable<RossmannProductDto> ReadProducts([EnumeratorCancellation]CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_config.Path, _config.FileName);
        await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        await foreach (var p in JsonSerializer.DeserializeAsyncEnumerable<RossmannProductDto>(stream, _jsonOptions, cancellationToken))
        {
            if (p is null)
            {
                continue;
            }
            yield return p;
        }
    }
}