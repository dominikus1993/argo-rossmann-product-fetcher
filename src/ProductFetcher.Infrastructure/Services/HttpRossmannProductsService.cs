using System.Runtime.CompilerServices;

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

    public async IAsyncEnumerable<RossmannProductDto> GetProductsInPromotion([EnumeratorCancellation]CancellationToken cancellationToken = default)
    {
        for (int page = 1; page < 3; page++)
        {
            var products = await _productsApi.GetProducts(page);
            if (products?.Data?.Products is null || products.Data.Products.Count == 0)
            {
                break;
            }
            foreach (var p in products.Data.Products)
            {
                yield return p.Adapt<ApiProductDto, RossmannProductDto>();
            }
        }
    }
}