using Microsoft.EntityFrameworkCore;
using v_conf_net.DTOs;
using v_conf_net.Models;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Services;

public class DefaultConfigService : IDefaultConfigService
{
    private readonly AppDbContext _context;

    public DefaultConfigService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DefaultConfigResponseDto?> GetDefaultConfigAsync(int modelId, int qty)
    {
        // 1️⃣ Fetch model
        var model = await _context.Models
            .FirstOrDefaultAsync(m => m.ModelId == modelId);

        if (model == null)
            return null;

        // 2️⃣ Fetch default components (Spring VehicleDefaultConfig table)
        var components = await _context.VehicleDefaultConfigs
    .Where(v => v.ModelId == modelId)
    .Include(v => v.Comp)   // join Component table
    .Select(v => new DefaultComponentDto
    {
        Name = v.Comp.CompName,   // from Component
        Price = (double)v.Comp.Price     // from Component
    })
    .ToListAsync();


        // 3️⃣ Price calculation
        var unitPrice = model.Price;
        var total = unitPrice * qty;

        // 4️⃣ Return DTO
        return new DefaultConfigResponseDto
        {
            ModelId = model.ModelId,
            ModelName = model.ModelName!,
            UnitPrice = unitPrice,
            Quantity = qty,
            TotalPrice = total,
            Components = components
        };
    }
}
