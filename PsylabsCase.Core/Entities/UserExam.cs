namespace PsylabsCase.Core.Entities;

public class UserExam
{
    public int UserId { get; set; }
    public int ExamId { get; set; }
    public string ExamLink { get; set; }
    public bool IsCompleted { get; set; }
    public double? Score { get; set; }
    public User User { get; set; }
    public Exam Exam { get; set; }
}