using System.Threading.Tasks;
using UserService.Repositories;
using UserService.User.Application.Services;

namespace UserService
{
    public class LoginService : ILoginService
    {
        protected IJwtTokenService _jwtTokenService;
        private readonly IRepository _repository;
        public LoginService(IJwtTokenService jwtTokenService, IRepository repository)
        {
            _repository =  repository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> Login(string username, string password)
        {
            // TODO: sprawdzanie danych w bazie
            var existingUser = await _repository.GetByUsernameAsync(username) ?? throw new InvalidCredentialsException();
            if (existingUser.Password != password)
            {
                throw new InvalidCredentialsException();
            }

            var roles = existingUser.Group;
            var token = _jwtTokenService.GenerateToken(existingUser.UserId, roles);

            return token;
        }
    }
}
