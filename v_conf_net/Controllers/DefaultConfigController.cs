using Microsoft.AspNetCore.Mvc;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Controllers;

[ApiController]
[Route("api/default-config")]
public class DefaultConfigController : ControllerBase
{
    private readonly IDefaultConfigService _service;

    public DefaultConfigController(IDefaultConfigService service)
    {
        _service = service;
    }

    // ======================================
    // GET /api/default-config/101?qty=5
    // ======================================
    [HttpGet("{modelId}")]
    public async Task<IActionResult> Get(int modelId, [FromQuery] int qty = 1)
    {
        var result = await _service.GetDefaultConfigAsync(modelId, qty);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
