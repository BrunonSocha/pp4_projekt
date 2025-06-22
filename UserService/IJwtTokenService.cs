namespace UserService
{
    public interface IJwtTokenService
    {
        string GenerateToken(Guid userId, string roles);
    }
}