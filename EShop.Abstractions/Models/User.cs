using EShopService;

namespace EShopAbstractions.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string Group { get; set; } = default!;

    public List<Order> Orders { get; set; } = new();
}