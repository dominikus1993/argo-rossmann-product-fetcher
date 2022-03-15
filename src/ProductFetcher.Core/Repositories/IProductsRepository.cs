using ProductFetcher.Core.Dto;

namespace ProductFetcher.Core.Repositories;

public interface IProductsWriter
{
    Task WriteProducts(IEnumerable<RossmannProductDto> products, CancellationToken cancellationToken = default);
}