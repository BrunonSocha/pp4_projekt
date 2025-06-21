
using EShopAbstractions;
using Microsoft.EntityFrameworkCore;

namespace UserService.Repositories
{
    public class Repository(EShopDbContext dbcontext) : IRepository
    {
        public async Task<EShopAbstractions.Models.User> AddAsync(EShopAbstractions.Models.User user)
        {
            await dbcontext.Users.AddAsync(user);
            await dbcontext.SaveChangesAsync();
            return user;
        }

        public async Task<EShopAbstractions.Models.User> GetByEmailAsync(string email)
        {
            return await dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<EShopAbstractions.Models.User> GetByIdAsync(Guid id)
        {
            return await dbcontext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<EShopAbstractions.Models.User> GetByUsernameAsync(string username)
        {
            return await dbcontext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
