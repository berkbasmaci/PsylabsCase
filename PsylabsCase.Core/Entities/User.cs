namespace PsylabsCase.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<UserExam> UserExams { get; set; }
}