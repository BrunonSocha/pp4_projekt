namespace EShopService;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string Group { get; set; } = default!;

    public List<Order> Orders { get; set; } = new();
}