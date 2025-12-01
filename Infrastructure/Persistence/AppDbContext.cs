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
                b.Property(x => x.Location).HasMaxLength(200);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
