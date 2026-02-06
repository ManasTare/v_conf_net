using v_conf_net.DTOs;

namespace v_conf_net.Services.Interfaces;

public interface IVehicleConfigService
{
    Task<List<ComponentDropdownDto>> GetConfigurableComponentsAsync(int modelId, string compType);
    Task SaveAlternateComponentsAsync(AlternateComponentSaveDto dto);
}
