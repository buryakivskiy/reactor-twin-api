using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactorTwinAPI.Application.Services;
using ReactorTwinAPI.Features.Users.Dtos;
using ReactorTwinAPI.Features.Users.Repositories;

namespace ReactorTwinAPI.Features.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUserService _currentUser;

        public UsersController(IUserRepository userRepo, ICurrentUserService currentUser)
        {
            _userRepo = userRepo;
            _currentUser = currentUser;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        public class UpdatePermissionsRequest
        {
            public bool? CanCreate { get; set; }
            public bool? IsSuper { get; set; }
        }

        [HttpPatch("{id}/permissions")]
        [Authorize]
        public async Task<IActionResult> UpdatePermissions(Guid id, [FromBody] UpdatePermissionsRequest req)
        {
            if (!_currentUser.IsSuperUser) return Forbid();

            var success = await _userRepo.UpdateAsync(id, user =>
            {
                if (req.CanCreate.HasValue) user.CanCreateReactor = req.CanCreate.Value;
                if (req.IsSuper.HasValue) user.IsSuperUser = req.IsSuper.Value;
            });

            if (!success) return NotFound();
            return NoContent();
        }
    }
}
