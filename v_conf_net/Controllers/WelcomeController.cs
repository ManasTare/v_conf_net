using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using v_conf_net.Models;

namespace v_conf_net.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WelcomeController : ControllerBase
{
    private readonly AppDbContext _context;

    public WelcomeController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/welcome/segments
    [HttpGet("segments")]
    public async Task<IActionResult> GetSegments()
    {
        var segments = await _context.Segments.ToListAsync();
        return Ok(segments);
    }
}
