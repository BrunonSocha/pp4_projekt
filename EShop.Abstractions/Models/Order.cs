using EShopAbstractions.Models;

namespace EShopAbstractions;

public class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = default!;

    public int CartId { get; set; }

    public Cart Cart { get; set; } = default!;
}