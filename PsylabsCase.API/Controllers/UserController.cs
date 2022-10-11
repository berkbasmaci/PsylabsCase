using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsylabsCase.API.Models;
using PsylabsCase.Service;
using PsylabsCase.Service.DTOs;
using PsylabsCase.Service.Services;

namespace PsylabsCase.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Create", Name = "Create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        if (ModelState.IsValid)
        {
            var result = _userService.CreateUser(new UserDto()
            {
                FullName = request.FullName,
                IsAdmin = request.IsAdmin,
                Password = request.Password,
                Username = request.Username
            });

            if (result == false)
                return BadRequest();
            
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("GetExamScores", Name = "GetExamScores")]
    public async Task<IActionResult> GetExamScores()
    {
        var result = _userService.GetExamScores();
        return Ok(result);
    }
}