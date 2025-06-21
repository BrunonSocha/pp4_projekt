using UserService.User.Application.Services;
using EShopAbstractions;

namespace UserService
{
    public class RegisterService : IRegisterService
    {
        private readonly IEShopDbContext _context;

        public RegisterService(IEShopDbContext context)
        {
            _context = context;
        }

        protected IJwtTokenService _jwtTokenService;
        public RegisterService(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        public void Register(Models.RegisterRequest request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
                throw new Exception("User already exists");

            var user = new EShopAbstractions.Models.User
            {
                Username = request.Username,
                Email = request.Email,
                Group = "users"
            };

            _context.Users.Add(user);
            _context.SaveChangesAsync();
        }
    }
}
