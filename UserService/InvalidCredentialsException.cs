
namespace UserService
{
    [Serializable]
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Incorrect username or login") { }
    }
}