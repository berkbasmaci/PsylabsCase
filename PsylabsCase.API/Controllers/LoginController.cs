using Microsoft.AspNetCore.Mvc;
using PsylabsCase.API.Models;
using PsylabsCase.Service;
using PsylabsCase.Service.Services;

namespace PsylabsCase.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AuthService _authenticationService;

        public LoginController(AuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            AuthResult authResult = _authenticationService.Authenticate(request.Username, request.Pwd);

            if (authResult.IsSuccessful == false)
                return BadRequest(authResult.ErrorMessage);

            var response = new LoginResponse
            {
                UserId = authResult.User.Id,
                FullName = authResult.User.FullName,
                Username = authResult.User.Username,
                IsAdmin = authResult.User.IsAdmin,
                Token = authResult.Token
            };

            return Ok(response);
        }
    }
}
