using System.ComponentModel.DataAnnotations;

namespace PsylabsCase.API.Models
{
    public class AddQuestionRequest
    {
        [Required]
        public int ExamId { get; set; }
        public QuestionRequest Question { get; set; }
    }

    public class QuestionRequest
    {
        [Required]
        public string QuestionText { get; set; }
        public List<AnswerRequest> Answers { get; set; }
    }

    public class AnswerRequest
    {
        [Required]
        public string Text { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
