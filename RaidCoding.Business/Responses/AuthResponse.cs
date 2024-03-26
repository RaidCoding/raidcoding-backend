using RaidCoding.Data.Models;

namespace RaidCoding.Logic.Responses;

public record AuthResponse(string Token, DateTime Expires, UserResponse User);

public static class AuthMappingExtensions
{
    public static AuthResponse ToAuthResponse(this User user, string token, DateTime expires)
    {
        return new AuthResponse(token, expires, user.ToResponse());
    }
}