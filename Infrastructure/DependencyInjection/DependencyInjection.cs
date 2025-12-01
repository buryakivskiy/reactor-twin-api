using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReactorTwinAPI.Application.Interfaces;
using ReactorTwinAPI.Infrastructure.Persistence;
using ReactorTwinAPI.Infrastructure.Repositories;

namespace ReactorTwinAPI.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IReactorTwinService, ReactorTwinService>();

            return services;
        }
    }
}
