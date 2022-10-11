namespace PsylabsCase.Service.DTOs;

public class UserExamScoreDto
{
    public int ExamId { get; set; }
    public DateTime ExamCreatedAt { get; set; }
    public int UserId { get; set; }
    public double? Score { get; set; }
    public string UserFullName { get; set; }
}