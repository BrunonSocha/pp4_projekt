namespace EShopService;

public class Product : BaseModel
{

    public string? Name { get; set; }
    public string? Ean { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? Sku { get; set; }
    public Category Category { get; set; } = default!;

    public int CategoryId { get; set; }
}