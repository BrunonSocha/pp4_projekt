namespace EShopService.Models;

public class Product : BaseModel
{
    public string Name { get; set; }

    public string Ean { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; } = 0;

    public string Sku { get; set; }

    public Category ProdCategory { get; set; }
}
