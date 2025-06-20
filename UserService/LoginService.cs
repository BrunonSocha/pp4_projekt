using UserService.User.Application.Services;

namespace UserService
{
    public class LoginService : ILoginService
    {
        protected IJwtTokenService _jwtTokenService;
        public LoginService(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        public string Login(string username, string password)
        {
            // TODO: sprawdzanie danych w bazie
            if (username != "admin" || password != "admin123")
            {
                throw new InvalidCredentialsException();
            }

            var roles = new List<string> { "Client", "Employee", "Administrator" };
            var token = _jwtTokenService.GenerateToken(123, roles);

            return token;
        }
    }
}
