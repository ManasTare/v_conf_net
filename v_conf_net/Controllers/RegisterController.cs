using Microsoft.AspNetCore.Mvc;
using v_conf_net.Models;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Controllers;

[Route("api/registration")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IUserService _service;

    public RegisterController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        return await _service.GetAllUsersAsync();
    }

    [HttpPost]
    public async Task<ActionResult<User>> Save([FromBody] User user)
    {
        var savedUser = await _service.RegisterUserAsync(user);
        return Ok(savedUser);
    }
}
