using ReactorTwinAPI.Features.ReactorTwins.Dtos;

namespace ReactorTwinAPI.Features.ReactorTwins.Repositories
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
