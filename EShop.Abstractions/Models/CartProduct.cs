using EShopAbstractions.Models;

namespace EShopService;

public class CartProduct
{
    public int CartId { get; set; }

    public Cart Cart { get; set; } = default!;

    public int ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public int Amount { get; set; }
}