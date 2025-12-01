using Microsoft.EntityFrameworkCore;
using ReactorTwinAPI.Domain.Entities;

namespace ReactorTwinAPI.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<ReactorTwin> ReactorTwins => Set<ReactorTwin>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReactorTwin>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.Name).IsRequired().HasMaxLength(200);
                b.Property(x => x.Model).IsRequired().HasMaxLength(200);

                b.Property(x => x.SerialNumber).IsRequired().HasMaxLength(100);
                b.Property(x => x.Version).IsRequired().HasMaxLength(50);
                b.Property(x => x.Status).IsRequired().HasMaxLength(50);
                b.Property(x => x.ReactorType).IsRequired().HasMaxLength(100);
                b.Property(x => x.FuelType).IsRequired().HasMaxLength(100);
                b.Property(x => x.CoolingSystemType).IsRequired().HasMaxLength(100);

                b.Property(x => x.ThermalOutputMW);
                b.Property(x => x.ElectricalOutputMW);
                b.Property(x => x.CoreTemperature);
                b.Property(x => x.PressureLevel);
                b.Property(x => x.CurrentTemperature);
                b.Property(x => x.CurrentPressure);
                b.Property(x => x.CurrentPowerOutput);
                b.Property(x => x.RadiationLevel);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
