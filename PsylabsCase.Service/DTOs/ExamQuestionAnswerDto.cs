namespace PsylabsCase.Service.DTOs;

public class ExamQuestionAnswerDto
{
    public int ExamId { get; set; }
    public int QuestionId { get; set; }
    public int UserId { get; set; }
    public int AnswerId { get; set; }
}