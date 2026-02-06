using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using v_conf_net.DTOs;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Controllers;

[Route("api/invoice")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpPost("confirm")]
    [Authorize] // Requires JWT Login
    public async Task<IActionResult> ConfirmOrder([FromBody] InvoiceRequestDto request)
    {
        try
        {
            // 1. Get User ID from Token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            
            if (int.TryParse(userIdClaim, out int authUserId))
            {
                request.UserId = authUserId; 
            }
            else
            {
                return BadRequest("Invalid User Token");
            }

            // 2. Call Service directly (Monolithic Logic)
            var result = await _invoiceService.GenerateInvoiceAsync(request);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error Generating Invoice: " + ex.Message);
        }
    }
}
