using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Projects.UpdateProject;
using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Application.Queries.ListMusicalProjects;
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
            IQueryHandler<ListMusicalProjectsQuery, ListMusicalProjectsResult>,
            ListMusicalProjectsHandler>();

        services.AddScoped<
            ICommandHandler<UpdateProjectCommand, UpdateProjectResult>,
            UpdateProjectCommandHandler>();

        return services;
    }
}
