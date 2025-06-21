namespace UserService
{
    public interface IRegisterService
    {
        Task<bool> Register(Models.RegisterRequest request);
    }
}
