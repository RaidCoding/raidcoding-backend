using RaidCoding.Data.Models;

namespace RaidCoding.Logic.Responses;

public record UserResponse(Guid Id, string Username, string Email, string? AvatarUrl, string? Bio);

public static class UserMappingExtensions
{
    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse(user.Id, user.UserName, user.Email, user.AvatarUrl, user.Bio);
    }
}