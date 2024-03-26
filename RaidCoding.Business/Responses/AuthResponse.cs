using RaidCoding.Data.Models;

namespace RaidCoding.Logic.Responses;

public record AuthResponse;

public static class AuthMappingExtensions
{
    public static AuthResponse ToAuthResponse(this User user)
    {
        throw new NotImplementedException();
    }
}