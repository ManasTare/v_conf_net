using v_conf_net.Models;

namespace v_conf_net.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> RegisterUserAsync(User user);
}
