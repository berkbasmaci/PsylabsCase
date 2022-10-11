using PsylabsCase.Core.Entities;

namespace PsylabsCase.Service.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<AnswerDto> AnswersDto { get; set; }
    }
}
