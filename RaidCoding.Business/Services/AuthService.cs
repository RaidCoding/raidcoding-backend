using Microsoft.AspNetCore.Identity;
using RaidCoding.Data;
using RaidCoding.Data.Models;
using RaidCoding.Infrastructure.Services;
using RaidCoding.Logic.Exceptions;
using RaidCoding.Logic.Responses;

namespace RaidCoding.Logic.Services;

public interface IAuthService
{
    Task<AuthResponse> Register(string userName, string password, string email, string? avatarUrl);
    Task<AuthResponse> Authenticate(string userName, string password);
}

public class AuthService(UserManager<User> userManager, JwtService jwtService) : IAuthService
{
    public async Task<AuthResponse> Register(string userName, string password, string email, string? avatarUrl)
    {
        var user = new User
        {
            UserName = userName,
            Email = email,
            AvatarUrl = avatarUrl
        };
        
        var result = await userManager.CreateAsync(user, password);
        
        if (!result.Succeeded)
        {
            throw new ValidationException(EntityName.User,
                result.Errors.ToDictionary(e => e.Code, e => new[] {e.Description}));
        }
        
        var (token, expires) = jwtService.GenerateToken(user);

        return user.ToAuthResponse(token, expires);
    }

    public async Task<AuthResponse> Authenticate(string userName, string password)
    {
        var user = await userManager.FindByNameAsync(userName);

        if (user is null)
        {
            throw new LogicalException(EntityName.User, ErrorType.NotFound.ToString(), "User not found",
                "The user with such username does not exist.");
        }
        
        var result = await userManager.CheckPasswordAsync(user, password);
        
        if (!result)
        {
            throw new LogicalException(EntityName.User, ErrorType.Restricted.ToString(), "Invalid password",
                "The password is incorrect.");
        }
        
        var (token, expires) = jwtService.GenerateToken(user);

        return user.ToAuthResponse(token, expires);
    }
}