using PsylabsCase.Core.Entities;
using PsylabsCase.Data;
using PsylabsCase.Service.DTOs;

namespace PsylabsCase.Service.Services;

public class AdminService
{
    private readonly PsylabsDbContext _context;

    public AdminService(PsylabsDbContext context)
    {
        _context = context;
    }
    
    public void SetExamToUser(UserExamDto userExamDto)
    {
        Guid link = Guid.NewGuid();
        UserExam userExam = new UserExam()
        {
            ExamId = userExamDto.ExamId,
            UserId = userExamDto.UserId,
            Score = 0,
            IsCompleted = false,
            ExamLink = "/exam/" + link.ToString()
        };

        _context.UserExams.Add(userExam);
        _context.SaveChanges();
    
    }
    public List<Exam> GetExams()
    {

        IQueryable<Exam> exams =
            from exam in _context.Exams
            select exam;

        List<Exam> result = exams.ToList();

        return result;
    }

    public void CreateExam(ExamDto examDto)
    {
        Exam exam = new Exam()
        {
            Name = examDto.Name,
        };

        _context.Exams.Add(exam);
        _context.SaveChanges();

    }

    public void AddQuestionToExam(QuestionDto questionDto, int examId)
    {
        Exam? exam = _context.Exams.FirstOrDefault(t => t.Id == examId);

        if (exam != null)
        {
            Question question = new()
            {
                Text = questionDto.Text,
                Answers = questionDto.AnswersDto.Select(t => new Answer()
                {
                    Text = t.Text,
                    IsCorrectAnswer = t.IsCorrectAnswer
                }).ToList(),
            };

            ExamQuestion examQuestion = new ExamQuestion()
            {
                Exam = exam,
                Question = question
            };

            _context.Questions.Add(question);
            _context.ExamQuestions.Add(examQuestion);

            _context.SaveChanges();
        }
    }
}