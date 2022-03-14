using ProductFetcher.Core.Dto;

namespace ProductFetcher.Core.Services;


public interface IRossmannProductsService
{
    IAsyncEnumerable<RossmannProductDto> GetProductsInPromotion();
}