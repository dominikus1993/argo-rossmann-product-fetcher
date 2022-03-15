using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

using ProductFetcher.Core.Dto;
using ProductFetcher.Core.Repositories;

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
        await System.IO.File.WriteAllTextAsync(path, json);
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
        var path = System.IO.Path.Combine(_config.Path, _config.FileName);
        await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        var products = JsonSerializer.DeserializeAsyncEnumerable<RossmannProductDto>(stream, _jsonOptions);
        await foreach (var p in products)
        {
            if (p is null)
            {
                continue;
            }
            yield return p;
        }
    }
}