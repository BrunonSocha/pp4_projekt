namespace UserService
{
    public interface IJwtTokenService
    {
        string GenerateToken(Guid userId, List<string> roles);
    }
}