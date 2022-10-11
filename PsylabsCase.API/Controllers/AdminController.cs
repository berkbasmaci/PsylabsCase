using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsylabsCase.API.Models;
using PsylabsCase.Service.DTOs;
using PsylabsCase.Service.Services;

namespace PsylabsCase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("SetExamToUser", Name = "SetExamToUser")]
        public async Task<IActionResult> SetExamToUser([FromBody] SetExamToUserRequest request)
        {
            if (ModelState.IsValid)
            {
                UserExamDto userExamDto = new UserExamDto()
                {
                    ExamId = request.ExamId,
                    UserId = request.UserId,
                };
                _adminService.SetExamToUser(userExamDto);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("CreateExam", Name = "CreateExam")]
        public async Task<IActionResult> CreateExam([FromBody]ExamCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                ExamDto examDto = new ExamDto()
                {
                    Name = request.Name,
                    CreatedAt = DateTime.UtcNow,
                };
                _adminService.CreateExam(examDto);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("AddQuestionToExam", Name = "AddQuestionToExam")]
        public async Task<IActionResult> AddQuestionToExam([FromBody]AddQuestionRequest request)
        {
            if (ModelState.IsValid)
            {
                QuestionDto questionDto = new()
                {
                    Text = request.Question.QuestionText,
                    AnswersDto = request.Question.Answers.Select(t => new AnswerDto()
                    {
                        Text = t.Text,
                        IsCorrectAnswer = t.IsCorrectAnswer
                    })
                };

                _adminService.AddQuestionToExam(questionDto, request.ExamId);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpGet("GetExams", Name = "GetExams")]
        public async Task<IActionResult> GetExams()
        {
            var result = _adminService.GetExams();
            return Ok(result);
        }

    }
}
