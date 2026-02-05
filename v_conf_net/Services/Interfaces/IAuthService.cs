using v_conf_net.DTOs;

namespace v_conf_net.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
}
