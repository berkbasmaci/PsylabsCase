namespace PsylabsCase.Service.DTOs;

public class UserExamDto
{
    public int UserId { get; set; }
    public int ExamId { get; set; }
    public string ExamLink { get; set; }
    public bool IsCompleted { get; set; }
    public double? Score { get; set; }
}