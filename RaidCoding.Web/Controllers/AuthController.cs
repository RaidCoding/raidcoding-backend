using Microsoft.AspNetCore.Mvc;
using RaidCoding.Logic.Responses;
using RaidCoding.Logic.Services;
using RaidCoding.Requests;

namespace RaidCoding.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService auth) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        var response = await auth.Register(request.Username, request.Password, request.Email, request.AvatarUrl);
        return Ok(response);
    }


    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        var response = await auth.Authenticate(request.Username, request.Password);
        return Ok(response);
    }
}