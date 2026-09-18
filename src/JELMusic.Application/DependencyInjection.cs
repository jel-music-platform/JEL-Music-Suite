using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Projects.UpdateProject;
using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Application.Queries.GetVideoProjectById;
using JELMusic.Application.Queries.ListMusicalProjects;
using JELMusic.Application.Queries.ListVideoProjects;
using JELMusic.Application.VideoProjects.CreateVideoProject;
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

        services.AddScoped<
            ICommandHandler<CreateVideoProjectCommand, Guid>,
            CreateVideoProjectCommandHandler>();

        services.AddScoped<
            IQueryHandler<ListVideoProjectsQuery, ListVideoProjectsResult>,
            ListVideoProjectsHandler>();

        services.AddScoped<
            IQueryHandler<GetVideoProjectByIdQuery, GetVideoProjectByIdResult>,
            GetVideoProjectByIdHandler>();

        return services;
    }
}
