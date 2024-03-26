using Microsoft.AspNetCore.Mvc;

namespace RaidCoding.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    [HttpGet]
    public Task<ActionResult> NotImplemented()
    {
        throw new NotImplementedException();
    }
}