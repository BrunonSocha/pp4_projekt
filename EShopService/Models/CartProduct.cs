namespace EShopService;

public class CartProduct
{
    public Guid CartUserId { get; set; }

    public Cart Cart { get; set; } = default!;

    public int ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public int Amount { get; set; }
}