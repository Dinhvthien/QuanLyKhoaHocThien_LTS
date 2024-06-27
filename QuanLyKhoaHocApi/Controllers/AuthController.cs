using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.RequestModels;

namespace QuanLyKhoaHocApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Request_Register request_Register) 
        {
            return Ok( await _authService.RegisterUser(request_Register));
        }

        [HttpPost("confirm-register-code")]
        public async Task<IActionResult> ConfirmRegisterCode(string code)
        {
            return Ok(await _authService.ConfirmRegisterUser(code));
        }
        [HttpPost("login")]

        public async Task<IActionResult> Login(Request_Login request_Login)
        {
            return Ok(await _authService.Login(request_Login));
        }

        [HttpPut("change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        public async Task<IActionResult> changePassword([FromBody] Request_ChangePassword request_changePassword)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _authService.ChangePassword(Id, request_changePassword));
        }

        [HttpPut("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            return Ok(await _authService.ForgotPassword(email));
        }
        [HttpPost("confirm-forgot-password")]

        public async Task<IActionResult> ConfirmForgotPassword([FromBody] Request_newPassword request_Login)
        {
            return Ok(await _authService.ConfirmForgotPassword(request_Login));
        }
        [HttpPut("update-info-user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateInfoUser([FromBody] Request_UpdateUser users)
        {
            int Id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _authService.UpdateInfoUser(Id, users));
        }

        [HttpGet("Get-info-user-by-id")]
        public async Task<IActionResult> GetInfoUser(int Id)
        {
            return Ok(await _authService.GetUserbyId(Id));
        }

        [HttpPost("lock-user")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Lockuser(int Id)
        {
            return Ok(await _authService.LockUser(Id));
        }
        [HttpPost("unlock-user")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UnLockuser(int Id)
        {
            return Ok(await _authService.UnlockUser(Id));
        }
    }
}
