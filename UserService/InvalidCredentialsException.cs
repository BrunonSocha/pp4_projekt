
namespace UserService.User.Application.Services
{
    [Serializable]
    internal class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Incorrect username or login") { }
    }
}