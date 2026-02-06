using Microsoft.EntityFrameworkCore;
using v_conf_net.Models;
using v_conf_net.Services.Interfaces;

namespace v_conf_net.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> RegisterUserAsync(User user)
    {
        // 1. Generate Registration No
        user.RegistrationNo = "VCONF-" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        // 2. Set Default Role
        user.Role = "USER";

        // 3. Save to DB
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // 4. (Optional) Mock Email Service call here

        return user;
    }
}
