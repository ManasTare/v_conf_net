using v_conf_net.DTOs;

namespace v_conf_net.Services.Interfaces;

public interface ILookupService
{
    Task<List<SegmentDto>> GetSegmentsAsync();
    Task<List<ManufacturerDto>> GetManufacturersAsync(int segId);
    Task<List<ModelDto>> GetModelsAsync(int segId, int mfgId);
}
