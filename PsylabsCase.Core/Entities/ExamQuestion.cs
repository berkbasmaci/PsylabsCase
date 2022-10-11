namespace PsylabsCase.Core.Entities;

public class ExamQuestion
{
    public int QuestionId { get; set; }
    public int ExamId { get; set; }
    public Question Question { get; set; }
    public Exam Exam { get; set; }
}