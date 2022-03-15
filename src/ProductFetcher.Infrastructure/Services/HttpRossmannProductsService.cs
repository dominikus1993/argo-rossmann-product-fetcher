using Mapster;

using ProductFetcher.Core.Dto;
using ProductFetcher.Core.Services;
using ProductFetcher.Infrastructure.Api;
using ProductFetcher.Infrastructure.Dto;

namespace ProductFetcher.Infrastructure.Services;

internal class HttpRossmannProductsService : IRossmannProductsService
{
    private readonly IProductsApi _productsApi;

    public HttpRossmannProductsService(IProductsApi productsApi)
    {
        _productsApi = productsApi;
    }

    public async IAsyncEnumerable<RossmannProductDto> GetProductsInPromotion()
    {
        int page = 1;
        while (true)
        {
            var products = await _productsApi.GetProducts(page);
            if (products?.Data is null || products.Data.Count == 0)
            {
                break;
            }
            foreach (var p in products.Data)
            {
                yield return p.Adapt<ApiProductDto, RossmannProductDto>();
            }
            page++;
        }
    }
}