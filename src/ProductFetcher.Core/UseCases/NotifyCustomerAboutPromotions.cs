using ProductFetcher.Core.Dto;
using ProductFetcher.Core.Repositories;
using ProductFetcher.Core.Services;

namespace ProductFetcher.Core.UseCases;

public sealed class NotifyCustomerAboutPromotionsUseCase
{
    private readonly IRossmannProductsService _productsService;
    private readonly IProductsWriter _productsWriter;

    public NotifyCustomerAboutPromotionsUseCase(IRossmannProductsService productsService, IProductsWriter productsWriter)
    {
        _productsService = productsService;
        _productsWriter = productsWriter;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var result = await _productsService.GetProductsInPromotion().ToListAsync();
        await _productsWriter.WriteProducts(result);
    }
}