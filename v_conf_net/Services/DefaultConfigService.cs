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

    public async Task<DefaultConfigResponseDto?> GetDefaultConfigurationAsync(int modelId, int qty)
    {
        var model = await _context.Models
            .FirstOrDefaultAsync(m => m.ModelId == modelId);

        if (model == null)
            return null;

        var unitPrice = model.Price;
        var total = unitPrice * qty;

        return new DefaultConfigResponseDto
        {
            ModelId = modelId,
            ModelName = model.ModelName!,
            UnitPrice = unitPrice,
            Quantity = qty,
            TotalPrice = total
        };
    }
}
