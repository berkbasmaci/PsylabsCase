namespace PsylabsCase.Service.DTOs;

public class AnswerDto
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; }
    public bool IsCorrectAnswer { get; set; }
}