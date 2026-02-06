using Microsoft.EntityFrameworkCore;
using v_conf_net.DTOs;
using v_conf_net.Models;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Services;

public class VehicleConfigService : IVehicleConfigService
{
    private readonly AppDbContext _context;

    public VehicleConfigService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ComponentDropdownDto>> GetConfigurableComponentsAsync(int modelId, string compType)
    {
        // 1. Fetch VehicleDetails matching Model, Type, and IsConfig='Y'
        // Include Component to get Name, Price, etc.
        var details = await _context.VehicleDetails
            .Where(vd => vd.ModelId == modelId 
                      && vd.CompType == compType 
                      && vd.IsConfig == "Y")
            .Include(vd => vd.Comp)
            .ToListAsync();

        // 2. Group by Component Name (e.g. "Color", "Rim Type")
        // Java Logic logic:
        // Map<String, List<OptionDto>> groupedMap = new LinkedHashMap<>();
        // For each detail -> get Comp -> Name -> Add Option(CompId, SubType, Price)

        var grouped = details
            .GroupBy(vd => vd.Comp.CompName)
            .Select(group => new ComponentDropdownDto(
                group.Key, // Component Name
                group.Select(vd => new OptionDto(
                    vd.Comp?.CompId ?? 0,
                    vd.Comp?.CompType ?? "Unknown", // Handle null CompType
                    vd.Comp?.Price ?? 0 // Handle nullable Price
                )).ToList()
            ))
            .ToList();

        return grouped;
    }

    public async Task SaveAlternateComponentsAsync(AlternateComponentSaveDto dto)
    {
        var model = await _context.Models.FindAsync(dto.ModelId);
        if (model == null) throw new Exception("Model not found");

        foreach (var item in dto.Components)
        {
            var original = await _context.Components.FindAsync(item.CompId);
            var alternate = await _context.Components.FindAsync(item.AltCompId);

            if (original == null || alternate == null)
            {
                 throw new Exception($"Component not found. Original: {item.CompId}, Alternate: {item.AltCompId}");
            }
            
            // Validation: Ensure we aren't replacing with unrelated type? 
            if (original.CompName != alternate.CompName) 
            {
                 throw new Exception($"Invalid component replacement: {original.CompName} vs {alternate.CompName}");
            }

            double deltaPrice = (alternate.Price ?? 0) - (original.Price ?? 0);

            // Check if exists
            var acm = await _context.AlternateComponentMasters
                .FirstOrDefaultAsync(a => a.ModelId == dto.ModelId && a.CompId == item.CompId);

            if (acm == null)
            {
                acm = new AlternateComponentMaster
                {
                    ModelId = dto.ModelId,
                    CompId = item.CompId
                };
                _context.AlternateComponentMasters.Add(acm);
            }

            // Update fields
            acm.AltCompId = item.AltCompId;
            acm.DeltaPrice = deltaPrice;
        }

        await _context.SaveChangesAsync();
    }
}
