using Microsoft.EntityFrameworkCore;
using v_conf_net.DTOs;
using v_conf_net.Models;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Services;

public class LookupService : ILookupService
{
    private readonly AppDbContext _context;

    public LookupService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SegmentDto>> GetSegmentsAsync()
    {
        return await _context.Segments
            .Select(s => new SegmentDto
            {
                Id = s.SegId,
                Name = s.SegName!
            })
            .ToListAsync();
    }

    public async Task<List<ManufacturerDto>> GetManufacturersAsync(int segId)
    {
        return await _context.SgMfgMasters
            .Where(x => x.SegId == segId)
            .Include(x => x.Mfg)
            .Select(x => new ManufacturerDto
            {
                Id = x.Mfg!.MfgId,
                Name = x.Mfg.MfgName!
            })
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<ModelDto>> GetModelsAsync(int segId, int mfgId)
    {
        return await _context.Models
            .Where(m => m.SegId == segId && m.MfgId == mfgId)
            .Select(m => new ModelDto
            {
                Id = m.ModelId,
                Name = m.ModelName!,
                Price = m.Price,
                MinQty = m.MinQty ?? 1,
                ImagePath = m.ImgPath
            })
            .ToListAsync();
    }
}
