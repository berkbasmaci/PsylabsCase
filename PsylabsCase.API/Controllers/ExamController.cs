using Microsoft.AspNetCore.Mvc;
using PsylabsCase.API.Models;
using PsylabsCase.API.Services;
using PsylabsCase.Service.DTOs;
using PsylabsCase.Service.Services;

namespace PsylabsCase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly LoginUserInfoProvider _loggedInUserService;

        public ExamController(
            UserService userService, 
            LoginUserInfoProvider loggedInUserService)
        {
            _userService = userService;
            _loggedInUserService = loggedInUserService;
        }

        [HttpGet("GetUserExamDetails", Name = "GetUserExamDetails")]
        public async Task<IActionResult> GetUserExamDetails([FromQuery]string? examLink)
        {
            if (string.IsNullOrWhiteSpace(examLink))
                examLink = "/exam/4403959f-a6c0-45fc-9021-d1089a100508";

            ExamDto exam = _userService.GetUserExamByExamLink(examLink);
            
            return Ok(exam);
        }

        [HttpGet("GetUserExams", Name = "GetUserExams")]
        public async Task<IActionResult> GetUserExams()
        {
            var userId = _loggedInUserService.UserId;
            var exams = _userService.GetExamsByUserId(userId);
            return Ok(exams);
        }

        [HttpGet("FinishExam", Name = "FinishExam")]
        public async Task<IActionResult> FinishExam([FromBody] FinishExamRequest request)
        {
            if (ModelState.IsValid)
            {
                int userId = _loggedInUserService.UserId;
                List<ExamQuestionAnswerDto> dtos = 
                    request.UserAnswers.Select(t => new ExamQuestionAnswerDto
                    {
                        ExamId = request.ExamId,
                        QuestionId = t.QuestionId,
                        UserId = userId,
                        AnswerId = t.AnswerId
                    }).ToList();

                double score = _userService.FinishExam(dtos);

                return Ok(new FinishExamResponse
                {
                    Score = score
                });
            }

            return BadRequest();
        }
    }
}