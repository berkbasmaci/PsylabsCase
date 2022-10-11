namespace PsylabsCase.Service.DTOs;

public class UserDto
{
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}