using Microsoft.EntityFrameworkCore;
using ReactorTwinAPI.Application.DTOs;
using ReactorTwinAPI.Application.Interfaces;
using ReactorTwinAPI.Domain.Entities;
using ReactorTwinAPI.Infrastructure.Persistence;

namespace ReactorTwinAPI.Infrastructure.Repositories
{
    public class ReactorTwinService : IReactorTwinService
    {
        private readonly AppDbContext _db;

        public ReactorTwinService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ReactorTwinDto> CreateAsync(CreateReactorTwinDto dto)
        {
            var entity = new ReactorTwin
            {
                Name = dto.Name,
                Model = dto.Model,
                Location = dto.Location ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            _db.ReactorTwins.Add(entity);
            await _db.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var e = await _db.ReactorTwins.FindAsync(id);
            if (e == null) return false;
            _db.ReactorTwins.Remove(e);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ReactorTwinDto>> GetAllAsync()
        {
            var list = await _db.ReactorTwins.AsNoTracking().ToListAsync();
            return list.Select(MapToDto);
        }

        public async Task<ReactorTwinDto?> GetByIdAsync(Guid id)
        {
            var e = await _db.ReactorTwins.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return e == null ? null : MapToDto(e);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateReactorTwinDto dto)
        {
            var e = await _db.ReactorTwins.FindAsync(id);
            if (e == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Name)) e.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.Model)) e.Model = dto.Model;
            if (dto.Location != null) e.Location = dto.Location;
            e.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        private ReactorTwinDto MapToDto(ReactorTwin e) => new ReactorTwinDto
        {
            Id = e.Id,
            Name = e.Name,
            Model = e.Model,
            Location = e.Location,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt
        };
    }
}
