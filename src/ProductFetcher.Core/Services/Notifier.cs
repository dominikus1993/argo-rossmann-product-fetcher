using ProductFetcher.Core.Dto;

namespace ProductFetcher.Core.Services;

public interface IPromotionNotifier
{
    ValueTask Notify(IEnumerable<RossmannProductDto> products, CancellationToken cancellationToken = default);
}