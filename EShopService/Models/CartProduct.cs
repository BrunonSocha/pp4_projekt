namespace EShopService;

public class CartProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string? Ean { get; set; }

    public string? Sku { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public int Quantity { get; set; }
}