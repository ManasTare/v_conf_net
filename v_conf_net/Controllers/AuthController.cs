using Microsoft.AspNetCore.Mvc;
using v_conf_net.DTOs;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);
        if (result == null)
        {
            return Unauthorized("Invalid credentials");
        }
        return Ok(result);
    }
}
