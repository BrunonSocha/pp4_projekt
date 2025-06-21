using EShopAbstractions.Models;
namespace UserService.Repositories
{
    public interface IRepository
    {
        Task<EShopAbstractions.Models.User> GetByIdAsync(Guid id);
        Task<EShopAbstractions.Models.User> GetByUsernameAsync(string username);
        Task<EShopAbstractions.Models.User> GetByEmailAsync(string email);
        Task<EShopAbstractions.Models.User> AddAsync(EShopAbstractions.Models.User user);
    }
}
