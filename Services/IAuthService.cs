namespace Administration.Services
{
    public interface IAuthService
    {
        string GenerateToken(string userId, string role);
    }

}
