namespace ProductFetcher.Infrastructure.Api;
using Refit;
using ProductFetcher.Infrastructure.Dto;
internal interface IProductsApi
{
    [Get("/api/Products?ShopNumber=735&PageSize=96&Page={page}&Statuses=promotion")]
    Task<RossmannApiResponse<List<ApiProductDto>>> GetProducts(int page);
}
