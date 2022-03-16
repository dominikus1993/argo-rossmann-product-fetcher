using ProductFetcher.Core.Dto;
using ProductFetcher.Core.Repositories;
using ProductFetcher.Core.Services;

namespace ProductFetcher.Core.UseCases;

public sealed class NotifyCustomerAboutPromotionsUseCase
{
    private readonly  IPromotionNotifier _notifier;
    private readonly IProductsReader _productsReader;

    public NotifyCustomerAboutPromotionsUseCase(IPromotionNotifier notifier, IProductsReader productsReader)
    {
        _notifier = notifier;
        _productsReader = productsReader;
    }
    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var products = await _productsReader.ReadProducts(cancellationToken).ToListAsync(cancellationToken: cancellationToken); ;
        await _notifier.Notify(products, cancellationToken);
    }
}