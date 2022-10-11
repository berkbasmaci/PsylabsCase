namespace PsylabsCase.Core.Entities;

public class Exam
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<ExamQuestion> ExamQuestions { get; set; }
    public ICollection<UserExam> UserExams { get; set; }
}