using PsylabsCase.Data;
using PsylabsCase.Service.DTOs;

namespace PsylabsCase.Service.Services;

public class ExamService
{
    private readonly PsylabsDbContext _context;

    public ExamService(PsylabsDbContext context)
    {
        _context = context;
    }
        
    public ExamDto GetExamDetails(int id)
    {
        var examQuery =
            from exam in _context.Exams
            where exam.Id == id
            select exam;

        var questionsQuery =
            from exam in examQuery
            join examQuestion in _context.ExamQuestions on exam.Id equals examQuestion.ExamId
            join question in _context.Questions on examQuestion.QuestionId equals question.Id
            select question;

        var answerQuery =
            from q in questionsQuery
            join a in _context.Answers on q.Id equals a.QuestionId
            select a;

        var examQueryResult = examQuery.FirstOrDefault();
        var questionsQueryResult = questionsQuery.ToList();
        var answersResult = answerQuery.ToList();
        var result = new ExamDto()
        {
            CreatedAt = examQueryResult.CreatedAt,
            Name = examQueryResult.Name,
            Id = examQueryResult.Id,
            QuestionDtos = questionsQueryResult.Select(q => new QuestionDto()
            {
                Id = q.Id,
                Text = q.Text,
                AnswersDto = answersResult.Where(t => t.QuestionId == q.Id).Select(t => new AnswerDto()
                {
                    Id = t.Id,
                    IsCorrectAnswer = t.IsCorrectAnswer,
                    QuestionId = t.QuestionId,
                    Text = t.Text,
                })
            })
        };

        return result;
    }
}