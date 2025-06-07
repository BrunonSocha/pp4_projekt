namespace EShopService;

public class Product : BaseModel
{

    public string? Ean { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? Sku { get; set; }

    public Category Category { get; set; } = default!;

}