namespace PsylabsCase.Core.Entities;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public ICollection<ExamQuestion> ExamQuestions { get; set; }
    public ICollection<Answer> Answers { get; set; }
}