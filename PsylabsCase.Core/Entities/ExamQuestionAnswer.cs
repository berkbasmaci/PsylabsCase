namespace PsylabsCase.Core.Entities
{
    public class ExamQuestionAnswer
    {
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
        public User User { get; set; }
        public Answer Answer { get; set; }
    }
}