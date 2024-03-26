using System.ComponentModel.DataAnnotations;

namespace RaidCoding.Requests;

public record LoginRequest(
    [Required] [Length(3, 16)] string Username,
    [Required] [Length(6, 36)] string Password);