using ReactorTwinAPI.Application.DTOs;
using ReactorTwinAPI.Application.Interfaces;

namespace ReactorTwinAPI.Application.Services
{
    public class ReactorTwinService : IReactorTwinService
    {
        private readonly IReactorTwinRepository _repository;

        public ReactorTwinService(IReactorTwinRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReactorTwinDto> CreateAsync(CreateReactorTwinDto dto)
        {
            return await _repository.CreateAsync(dto);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReactorTwinDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ReactorTwinDto?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateReactorTwinDto dto)
        {
            return await _repository.UpdateAsync(id, dto);
        }
    }
}
