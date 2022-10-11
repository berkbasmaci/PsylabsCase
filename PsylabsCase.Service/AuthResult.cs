using PsylabsCase.Core.Entities;

namespace PsylabsCase.Service;

public class AuthResult
{
    public User User { get; set; }
    public string Token { get; set; }
    public string ErrorMessage { get; set; }
    public bool IsSuccessful { get; set; }
}