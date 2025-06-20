namespace UserService
{
    public interface IJwtTokenService
    {
        string GenerateToken(int userId, List<string> roles);
    }
}