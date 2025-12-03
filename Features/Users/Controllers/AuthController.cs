using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReactorTwinAPI.Features.Users.Dtos;
using ReactorTwinAPI.Features.Users.Repositories;

namespace ReactorTwinAPI.Features.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            var any = await _userRepo.AnyUsersAsync();
            if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
                return BadRequest("Username and password are required");

            var exists = await _userRepo.GetByUsernameAsync(req.Username);
            if (exists != null) return Conflict("Username already exists");

            var dto = new CreateUserDto { Username = req.Username };
            if (!any && req.RequestSuper)
            {
                dto.IsSuperUser = true;
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(req.Password);
            var created = await _userRepo.CreateAsync(dto, hash);
            return Ok(created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
                return BadRequest("Username and password are required");

            var userEntity = await _userRepo.GetEntityByUsernameAsync(req.Username);
            if (userEntity == null) return Unauthorized();

            if (!BCrypt.Net.BCrypt.Verify(req.Password, userEntity.PasswordHash)) return Unauthorized();

            var token = GenerateToken(userEntity.Id, userEntity.Username, userEntity.IsSuperUser, userEntity.CanCreateReactor);
            return Ok(new { token });
        }

        private string GenerateToken(Guid userId, string username, bool isSuper, bool canCreate)
        {
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key)) throw new InvalidOperationException("Jwt:Key is not configured");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim("isSuper", isSuper ? "true" : "false"),
                new Claim("canCreate", canCreate ? "true" : "false")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
