using System.ComponentModel.DataAnnotations;

namespace RaidCoding.Requests;

public record RegisterRequest(
    [Required] [Length(3, 16)] string Username,
    [EmailAddress] [Required] string Email,
    [Required] [Length(6, 36)] string Password,
    [Url] string? AvatarUrl);