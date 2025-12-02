using Microsoft.EntityFrameworkCore;
using ReactorTwinAPI.Infrastructure.Persistence;
using ReactorTwinAPI.Features.ReactorTwins.Repositories;
using ReactorTwinAPI.Features.ReactorTwins.Services;

namespace ReactorTwinAPI.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IReactorTwinRepository, ReactorTwinRepository>();
            services.AddScoped<IReactorTwinService, ReactorTwinService>();

            return services;
        }
    }
}
