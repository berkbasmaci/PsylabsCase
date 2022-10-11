using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PsylabsCase.Core.Entities;
using PsylabsCase.Data;
using PsylabsCase.Service.DTOs;

namespace PsylabsCase.Service.Services;

public class UserService 
{
    private readonly PsylabsDbContext _context;

    public UserService(PsylabsDbContext context)
    {
        _context = context;
    }

    public double FinishExam(List<ExamQuestionAnswerDto> examQuestionAnswerDtos)
    {
        var examQuestionAnswerDto = examQuestionAnswerDtos.FirstOrDefault();

        if (examQuestionAnswerDto == null)
            return 0;

        var answers =
            (from answer in _context.Answers
            join examQ in _context.ExamQuestions
                on answer.QuestionId equals examQ.QuestionId
            where examQ.ExamId == examQuestionAnswerDto.ExamId
            select answer).ToList();

        var correctAnswers =
            (from answer in answers
            join examQuDto in examQuestionAnswerDtos
                on answer.QuestionId equals examQuDto.QuestionId
            where answer.IsCorrectAnswer == true
            select answer).ToList();

        var userExam = _context.UserExams
            .FirstOrDefault(t => t.UserId == examQuestionAnswerDto.UserId &&
                                 t.ExamId == examQuestionAnswerDto.ExamId);

        if (userExam == null)
            return 0;

        List<ExamQuestionAnswer> entities =
            examQuestionAnswerDtos.Select(dto => new ExamQuestionAnswer
            {
                ExamId = dto.ExamId,
                QuestionId = dto.QuestionId,
                UserId = dto.UserId,
                AnswerId = dto.AnswerId,
            }).ToList();
        
        userExam.IsCompleted = true;
        userExam.Score = (100 / answers.Count) * correctAnswers.Count;

        _context.ExamQuestionAnswers.AddRange(entities);
        _context.SaveChanges();
        
        return 1d;
    }


    public ExamDto GetUserExamByExamLink(string examLink)
    {
        var examQuery = 
            from userExam in _context.UserExams
            join exam in _context.Exams on userExam.ExamId equals exam.Id
            where userExam.ExamLink == examLink
            select exam;

        var examQueryResult = examQuery.FirstOrDefault();

        var questionsWithAnswers =
            (from examQuestion in _context.ExamQuestions
            join question in _context.Questions on examQuestion.QuestionId equals question.Id
            join answer in _context.Answers on question.Id equals answer.QuestionId
            where examQuestion.ExamId == examQueryResult.Id
            group answer by new { question.Id, question.Text } into ansGrp
            select new Question()
            {
                Answers = ansGrp.ToList(),
                Id = ansGrp.Key.Id,
                Text = ansGrp.Key.Text
            }).ToList();

        var result = new ExamDto()
        {
            CreatedAt = examQueryResult.CreatedAt,
            Name = examQueryResult.Name,
            Id = examQueryResult.Id,
            QuestionDtos = questionsWithAnswers.Select(q => new QuestionDto()
            {
                Id = q.Id,
                Text = q.Text,
                AnswersDto = q.Answers.Select(t => new AnswerDto()
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

    public IEnumerable<Exam> GetExamsByUserId(int userId)
    {
        IQueryable<Exam> q =
            from ex in _context.Exams
            join userEx in _context.UserExams on userId equals userEx.UserId
            select ex;

        List<Exam> r = q.ToList();

        return r;
    }

    public List<UserDto> GetUsers()
    {
        var userQuery =
            from user in _context.Users
            select user;
        var examQueryResult = userQuery.ToList();

        var result = examQueryResult.Select(t => new UserDto()
        {
            Password = t.Password,
            Username = t.Username,
        });

        return result.ToList();
    }

    public bool CreateUser(UserDto userDto)
    {
        _context.Users.Add(new User
        {
            FullName = userDto.FullName,
            IsAdmin = userDto.IsAdmin,
            Password = userDto.Password,
            Username = userDto.Username,
        });

        int result = _context.SaveChanges();

        return result != 0;
    }

    public IEnumerable<UserExamScoreDto> GetExamScores()
    {
        IQueryable<UserExamScoreDto> x =
            from exam in _context.Exams
            join userExam in _context.UserExams on exam.Id equals userExam.ExamId
            join user in _context.Users on userExam.UserId equals user.Id
            select new UserExamScoreDto
            {
                ExamId = exam.Id,
                ExamCreatedAt = exam.CreatedAt,
                UserId = user.Id,
                UserFullName = user.FullName,
                Score = userExam.Score
            };

        List<UserExamScoreDto> r = x.ToList();

        return r;
    }
}