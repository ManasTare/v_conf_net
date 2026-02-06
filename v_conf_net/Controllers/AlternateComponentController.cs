using Microsoft.AspNetCore.Mvc;
using v_conf_net.DTOs;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Controllers;

[Route("api/alternate-component")]
[ApiController]
public class AlternateComponentController : ControllerBase
{
    private readonly IVehicleConfigService _service;

    public AlternateComponentController(IVehicleConfigService service)
    {
        _service = service;
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveAlternateComponent([FromBody] AlternateComponentSaveDto dto)
    {
        try
        {
            if (dto == null) return BadRequest("DTO is null");
            if (dto.Components == null) return BadRequest("Components list is null");

            await _service.SaveAlternateComponentsAsync(dto);
            return Ok("Alternate components saved successfully"); 
        }
        catch (Exception ex)
        {
            return BadRequest("Error saving configuration: " + ex.Message);
        }
    }
}
