using ReactorTwinAPI.Application.DTOs;

namespace ReactorTwinAPI.Application.Interfaces
{
    public interface IReactorTwinRepository
    {
        Task<ReactorTwinDto> CreateAsync(CreateReactorTwinDto dto);
        Task<ReactorTwinDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ReactorTwinDto>> GetAllAsync();
        Task<bool> UpdateAsync(Guid id, UpdateReactorTwinDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
