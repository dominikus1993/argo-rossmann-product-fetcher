using ProductFetcher.Core.Dto;

namespace ProductFetcher.Core.Repositories;

public interface IProductsWriter
{
    Task WriteProducts(IEnumerable<RossmannProductDto> products, CancellationToken cancellationToken = default);
}

public interface IProductsReader
{
    IAsyncEnumerable<RossmannProductDto> ReadProducts(CancellationToken cancellationToken = default);
}