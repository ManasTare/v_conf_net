namespace v_conf_net.DTOs;

public class LoginRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // In real app, we verify hash
}
