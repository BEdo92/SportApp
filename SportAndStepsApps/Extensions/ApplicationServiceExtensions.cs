using Microsoft.EntityFrameworkCore;
using SportAndStepsApps.Data;
using SportAndStepsApps.Interfaces;
using SportAndStepsApps.Repositories;
using SportAndStepsApps.Services;

namespace SportAndStepsApps.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddDbContext<SportsContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserActivityRepository, UserActivityRepository>();
        services.AddScoped<ISportRepository, SportRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
