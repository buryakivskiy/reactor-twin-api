using Microsoft.EntityFrameworkCore;
using ReactorTwinAPI.Application.DTOs;
using ReactorTwinAPI.Application.Interfaces;
using ReactorTwinAPI.Domain.Entities;
using ReactorTwinAPI.Infrastructure.Persistence;

namespace ReactorTwinAPI.Infrastructure.Repositories
{
    public class ReactorTwinRepository : IReactorTwinRepository
    {
        private readonly AppDbContext _db;

        public ReactorTwinRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ReactorTwinDto> CreateAsync(CreateReactorTwinDto dto)
        {
            var reactor = new ReactorTwin
            {
                Name = dto.Name,
                Model = dto.Model,

                SerialNumber = dto.SerialNumber,
                Version = dto.Version,
                Status = dto.Status,

                ReactorType = dto.ReactorType,
                ThermalOutputMW = dto.ThermalOutputMW,
                ElectricalOutputMW = dto.ElectricalOutputMW,

                FuelType = dto.FuelType,

                CoreTemperature = dto.CoreTemperature,
                PressureLevel = dto.PressureLevel,
                CoolingSystemType = dto.CoolingSystemType,

                CurrentTemperature = dto.CurrentTemperature,
                CurrentPressure = dto.CurrentPressure,
                CurrentPowerOutput = dto.CurrentPowerOutput,
                RadiationLevel = dto.RadiationLevel,

                CreatedAt = DateTime.UtcNow
            };

            _db.ReactorTwins.Add(reactor);
            await _db.SaveChangesAsync();

            return MapToDto(reactor);
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
            var reactor = await _db.ReactorTwins.FindAsync(id);
            if (reactor == null)
            {
                return false;
            }

            reactor.Name = dto.Name;
            reactor.Model = dto.Model;
            reactor.SerialNumber = dto.SerialNumber;
            reactor.Version = dto.Version;
            reactor.Status = dto.Status;
            reactor.ReactorType = dto.ReactorType;
            reactor.ThermalOutputMW = dto.ThermalOutputMW;
            reactor.ElectricalOutputMW = dto.ElectricalOutputMW;
            reactor.FuelType = dto.FuelType;
            reactor.CoreTemperature = dto.CoreTemperature;
            reactor.PressureLevel = dto.PressureLevel;
            reactor.CoolingSystemType = dto.CoolingSystemType;
            reactor.CurrentTemperature = dto.CurrentTemperature;
            reactor.CurrentPressure = dto.CurrentPressure;
            reactor.CurrentPowerOutput = dto.CurrentPowerOutput;
            reactor.RadiationLevel = dto.RadiationLevel;

            reactor.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return true;
        }


        private ReactorTwinDto MapToDto(ReactorTwin reactor)
        {
            return new ReactorTwinDto
            {
                Id = reactor.Id,
                Name = reactor.Name,
                Model = reactor.Model,
                SerialNumber = reactor.SerialNumber,
                Version = reactor.Version,
                Status = reactor.Status,
                ReactorType = reactor.ReactorType,
                ThermalOutputMW = reactor.ThermalOutputMW,
                ElectricalOutputMW = reactor.ElectricalOutputMW,
                FuelType = reactor.FuelType,
                CoreTemperature = reactor.CoreTemperature,
                PressureLevel = reactor.PressureLevel,
                CoolingSystemType = reactor.CoolingSystemType,
                CurrentTemperature = reactor.CurrentTemperature,
                CurrentPressure = reactor.CurrentPressure,
                CurrentPowerOutput = reactor.CurrentPowerOutput,
                RadiationLevel = reactor.RadiationLevel,
                CreatedAt = reactor.CreatedAt,
                UpdatedAt = reactor.UpdatedAt
            };
        }
    }
}
