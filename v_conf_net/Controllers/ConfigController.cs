using Microsoft.AspNetCore.Mvc;
using v_conf_net.DTOs;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Controllers;

[ApiController]
[Route("api/config")]
public class ConfigController : ControllerBase
{
    private readonly IDefaultConfigService _configService;

    public ConfigController(IDefaultConfigService configService)
    {
        _configService = configService;
    }

    // ========================================
    // POST: /api/config/default-config
    // ========================================
    [HttpPost("default-config")]
    public async Task<IActionResult> GetDefaultConfig(
        [FromBody] DefaultConfigRequestDto request)
    {
        var result = await _configService
            .GetDefaultConfigurationAsync(request.ModelId, request.Quantity);

        if (result == null)
            return NotFound("Model not found");

        return Ok(result);
    }
}
