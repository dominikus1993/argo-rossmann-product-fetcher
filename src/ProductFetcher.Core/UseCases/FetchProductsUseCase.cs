using ProductFetcher.Core.Dto;
using ProductFetcher.Core.Services;

namespace ProductFetcher.Core.UseCases;

public sealed class FetchRossmannProductsUseCase
{
    private readonly IRossmannProductsService _productsService;

    public FetchRossmannProductsUseCase(IRossmannProductsService productsService)
    {
        _productsService = productsService;
    }

    public async Task<List<RossmannProductDto>> Execute()
    {
        return await _productsService.GetProductsInPromotion().ToListAsync();
    }
}