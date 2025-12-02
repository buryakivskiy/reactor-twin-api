using Microsoft.AspNetCore.Mvc;
using ReactorTwinAPI.Features.ReactorTwins.Dtos;
using ReactorTwinAPI.Features.ReactorTwins.Services;

namespace ReactorTwinAPI.Features.ReactorTwins.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactorTwinsController : ControllerBase
    {
        private readonly IReactorTwinService _service;

        public ReactorTwinsController(IReactorTwinService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReactorTwinDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _service.GetByIdAsync(id);
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _service.GetAllAsync();
            return Ok(res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReactorTwinDto dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
