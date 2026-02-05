using v_conf_net.Models;

namespace v_conf_net.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    // We can return the User object or a specific UserDto. 
    // To match Java, we might just return the user details embedded or separate.
    public User? User { get; set; }
}
