using JELMusic.Application.UseCases.CreateMusicalProject;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateMusicalProjectUseCase>();

        return services;
    }
}