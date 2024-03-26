using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RaidCoding.Data.Models;

namespace RaidCoding.Infrastructure.Services;

public class JwtService(IConfiguration configuration)
{
    public (string, DateTime) GenerateToken(User user)
    {
        var claims = GetClaims(user);

        var signingKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? throw new ArgumentNullException()));

        var token = new JwtSecurityToken(
            configuration["JWT:ValidIssuer"],
            configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

        return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
    }

    private static IEnumerable<Claim> GetClaims(User user)
    {
        return new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.UserName)
        };
    }
}