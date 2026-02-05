using v_conf_net.DTOs;

namespace v_conf_net.Services.Interfaces;

public interface IDefaultConfigService
{
    Task<DefaultConfigResponseDto?> GetDefaultConfigurationAsync(int modelId, int qty);
}
