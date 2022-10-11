namespace PsylabsCase.Service.DTOs;

public class ExamDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<QuestionDto> QuestionDtos { get; set; }
}