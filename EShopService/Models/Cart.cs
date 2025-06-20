namespace EShopService;

public class Cart
{ 
    public int CartId { get; set; }
    public Guid UserId { get; set; }

    public List<CartProduct> Items { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public bool Deleted { get; set; } = false;
}