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