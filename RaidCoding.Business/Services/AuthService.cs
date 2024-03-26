using RaidCoding.Logic.Responses;

namespace RaidCoding.Logic.Services;




public interface IAuthService
{
    Task<AuthResponse> Register(string userName, string password, string email);
    Task<AuthResponse> Authenticate(string userName, string password);
}

public class AuthService : IAuthService
{
    public Task<AuthResponse> Register(string userName, string password, string email)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponse> Authenticate(string userName, string password)
    {
        throw new NotImplementedException();
    }
}