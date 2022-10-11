namespace PsylabsCase.API.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}
