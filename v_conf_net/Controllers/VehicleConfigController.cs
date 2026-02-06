using Microsoft.AspNetCore.Mvc;
using v_conf_net.DTOs;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Controllers;

[Route("api/vehicle")]
[ApiController]
public class VehicleConfigController : ControllerBase
{
    private readonly IVehicleConfigService _service;

    public VehicleConfigController(IVehicleConfigService service)
    {
        _service = service;
    }

    [HttpGet("{modelId}/standard")]
    public async Task<ActionResult<List<ComponentDropdownDto>>> GetStandardComponents(int modelId)
    {
        var list = await _service.GetConfigurableComponentsAsync(modelId, "S");
        return Ok(list);
    }

    [HttpGet("{modelId}/interior")]
    public async Task<ActionResult<List<ComponentDropdownDto>>> GetInteriorComponents(int modelId)
    {
        var list = await _service.GetConfigurableComponentsAsync(modelId, "I");
        return Ok(list);
    }

    [HttpGet("{modelId}/exterior")]
    public async Task<ActionResult<List<ComponentDropdownDto>>> GetExteriorComponents(int modelId)
    {
        var list = await _service.GetConfigurableComponentsAsync(modelId, "E");
        return Ok(list);
    }

    [HttpGet("{modelId}/accessories")]
    public async Task<ActionResult<List<ComponentDropdownDto>>> GetAccessoryComponents(int modelId)
    {
        var list = await _service.GetConfigurableComponentsAsync(modelId, "C");
        return Ok(list);
    }
}
