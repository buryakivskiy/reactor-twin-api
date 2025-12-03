using Microsoft.AspNetCore.Mvc;
using ReactorTwinAPI.Features.Auth.Dtos;

namespace ReactorTwinAPI.Features.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly Features.Auth.Services.IAuthService _authService;

        public AuthController(Features.Auth.Services.IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto req)
        {
            try
            {
                var created = await _authService.RegisterAsync(req);
                return Ok(created);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (InvalidOperationException ie)
            {
                return Conflict(ie.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto req)
        {
            try
            {
                var token = await _authService.LoginAsync(req);
                if (token == null) return Unauthorized();
                return Ok(new { token });
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }
    }
}
