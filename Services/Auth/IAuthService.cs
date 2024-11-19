namespace Administration.Services.Auth
{
    public interface IAuthService
    {
        string GenerateToken(string userId, string role);
    }

}
