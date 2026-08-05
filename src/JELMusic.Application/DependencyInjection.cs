using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<
            ICommandHandler<CreateProjectCommand, Guid>,
            CreateProjectCommandHandler>();

        services.AddScoped<
            IQueryHandler<GetMusicalProjectByIdQuery, GetMusicalProjectByIdResult>,
            GetMusicalProjectByIdHandler>();

        services.AddScoped<
            IMusicalProjectFactory,
            MusicalProjectFactory>();  

        return services;
    }
}