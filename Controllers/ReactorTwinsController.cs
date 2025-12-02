using Microsoft.AspNetCore.Mvc;
using ReactorTwinAPI.Application.DTOs;
using ReactorTwinAPI.Application.Interfaces;

namespace ReactorTwinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactorTwinsController : ControllerBase
    {
        private readonly IReactorTwinRepository _repository;

        public ReactorTwinsController(IReactorTwinRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReactorTwinDto dto)
        {
            var created = await _repository.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _repository.GetByIdAsync(id);
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _repository.GetAllAsync();
            return Ok(res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReactorTwinDto dto)
        {
            var ok = await _repository.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _repository.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
