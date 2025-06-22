using UserService.User.Application.Services;
using EShopAbstractions;
using UserService.Repositories;
using System.Threading.Tasks;

namespace UserService
{
    public class RegisterService : IRegisterService
    {
        private readonly IEShopDbContext _context;
        private readonly IRepository _repository;
        protected IJwtTokenService _jwtTokenService;

        public RegisterService(IEShopDbContext context, IRepository repository, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _repository = repository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<bool> Register(Models.RegisterRequest request)
        {
            var email = await _repository.GetByEmailAsync(request.Email);
            var username = await _repository.GetByUsernameAsync(request.Username);

            if (email != null || username != null)
            {
                throw new Exception("Username already exists");
            }

            var user = new EShopAbstractions.Models.User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Group = "users"
            };

            await _repository.AddAsync(user);
            return true;
        }
    }
}
