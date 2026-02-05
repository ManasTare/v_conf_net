using Microsoft.AspNetCore.Mvc;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Controllers;

[ApiController]
[Route("api/welcome")]
public class WelcomeController : ControllerBase
{
    private readonly ILookupService _lookupService;

    public WelcomeController(ILookupService lookupService)
    {
        _lookupService = lookupService;
    }

    // ========================================
    // GET: /api/welcome/segments
    // ========================================
    [HttpGet("segments")]
    public async Task<IActionResult> GetSegments()
    {
        var data = await _lookupService.GetSegmentsAsync();
        return Ok(data);
    }

    // ========================================
    // GET: /api/welcome/manufacturers/{segId}
    // ========================================
    [HttpGet("manufacturers/{segId}")]
    public async Task<IActionResult> GetManufacturers(int segId)
    {
        var data = await _lookupService.GetManufacturersAsync(segId);
        return Ok(data);
    }

    // ========================================
    // GET: /api/welcome/models?segId=1&mfgId=2
    // ========================================
    [HttpGet("models")]
    public async Task<IActionResult> GetModels(
        [FromQuery] int segId,
        [FromQuery] int mfgId)
    {
        var data = await _lookupService.GetModelsAsync(segId, mfgId);
        return Ok(data);
    }
}
