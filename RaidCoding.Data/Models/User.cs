using Microsoft.AspNetCore.Identity;

namespace RaidCoding.Data.Models;

public class User : IdentityUser<Guid>
{
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public DateTime CreatedAt { get; set; }
}