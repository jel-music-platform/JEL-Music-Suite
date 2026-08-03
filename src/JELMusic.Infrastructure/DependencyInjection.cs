using JELMusic.Domain.Repositories;
using JELMusic.Infrastructure.Persistence;
using JELMusic.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Infrastructure.Dispatching;

namespace JELMusic.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<CoreDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IMusicalProjectRepository, MusicalProjectRepository>();
        services.AddScoped<IUnitOfWork, CoreUnitOfWork>();

        services.AddScoped<IApplicationDispatcher, ApplicationDispatcher>();
 

        return services;
    }
}