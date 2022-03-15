namespace ProductFetcher.Infrastructure.Dto;

internal class RossmannApiResponse<T>
{
    public T? Data { get; set; }
}

internal class ApiProductDto
{
    public int Id { get; set; }
    public string? NavigateUrl { get; set; }
    public string? Name { get; set; }
    public string? Caption { get; set; }
    public string? Unit { get; set; }
    public string? Brand { get; set; }
    public double? OldPrice { get; set; }
    public double Price { get; set; }
    public decimal? LoyaltyPrice { get; set; }
    public string? PricePerUnit { get; set; }
    public string? LoyaltyPricePerUnit { get; set; }
    public string? DANNumber { get; set; }
    public string? AGroupNrOld { get; set; }
    public string? Vat { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public double Depth { get; set; }
    public double Weight { get; set; }
    public string? Category { get; set; }
}