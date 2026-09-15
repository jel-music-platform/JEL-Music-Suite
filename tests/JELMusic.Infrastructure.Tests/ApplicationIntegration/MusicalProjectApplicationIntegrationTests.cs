using JELMusic.Application;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Projects.UpdateProject;
using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Application.Queries.ListMusicalProjects;
using JELMusic.Domain.Repositories;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;
using JELMusic.Framework;
using JELMusic.Infrastructure.Persistence;
using JELMusic.Infrastructure.Persistence.Repositories;
using JELMusic.Infrastructure.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Infrastructure.Tests.ApplicationIntegration;

public sealed class MusicalProjectApplicationIntegrationTests
{
    [Fact]
    public async Task Create_then_get_should_persist_project()
    {
        using var database = new SqliteTestDatabase();
        using var serviceProvider = CreateServiceProvider(database);

        using var scope = serviceProvider.CreateScope();

        var dispatcher = scope.ServiceProvider
            .GetRequiredService<IApplicationDispatcher>();

        var command = new CreateProjectCommand(
            "Proyecto integración",
            "Worship",
            "Descripción de integración",
            CreateMusicalDNA());

        var projectId = await dispatcher.SendCommandAsync<
            CreateProjectCommand,
            Guid>(command);

        var result = await dispatcher.SendQueryAsync<
            GetMusicalProjectByIdQuery,
            GetMusicalProjectByIdResult>(
            new GetMusicalProjectByIdQuery(projectId));

        Assert.NotNull(result);
        Assert.Equal(projectId, result!.ProjectId);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.Genre, result.Genre);
        Assert.Equal(command.Description, result.Description);
    }

    [Fact]
    public async Task Update_then_get_should_persist_changes()
    {
        using var database = new SqliteTestDatabase();
        using var serviceProvider = CreateServiceProvider(database);

        using var scope = serviceProvider.CreateScope();

        var dispatcher = scope.ServiceProvider
            .GetRequiredService<IApplicationDispatcher>();

        var dna = CreateMusicalDNA();

        var projectId = await dispatcher.SendCommandAsync<
            CreateProjectCommand,
            Guid>(
            new CreateProjectCommand(
                "Nombre original",
                "Pop",
                "Descripción original",
                dna));

        var updateResult = await dispatcher.SendCommandAsync<
            UpdateProjectCommand,
            UpdateProjectResult>(
            new UpdateProjectCommand(
                projectId,
                "Nombre actualizado",
                "Worship",
                "Descripción actualizada",
                dna));

        Assert.Equal(projectId, updateResult.ProjectId);
        Assert.Equal(2, updateResult.ProjectVersion);

        database.Context.ChangeTracker.Clear();

        var result = await dispatcher.SendQueryAsync<
            GetMusicalProjectByIdQuery,
            GetMusicalProjectByIdResult>(
            new GetMusicalProjectByIdQuery(projectId));

        Assert.NotNull(result);
        Assert.Equal(projectId, result!.ProjectId);
        Assert.Equal("Nombre actualizado", result.Name);
        Assert.Equal("Worship", result.Genre);
        Assert.Equal("Descripción actualizada", result.Description);
    }

    [Fact]
    public async Task List_should_return_persisted_projects()
    {
        using var database = new SqliteTestDatabase();
        using var serviceProvider = CreateServiceProvider(database);

        using var scope = serviceProvider.CreateScope();

        var dispatcher = scope.ServiceProvider
            .GetRequiredService<IApplicationDispatcher>();

        await dispatcher.SendCommandAsync<
            CreateProjectCommand,
            Guid>(
            new CreateProjectCommand(
                "Proyecto uno",
                "Pop",
                "Descripción uno",
                CreateMusicalDNA()));

        await dispatcher.SendCommandAsync<
            CreateProjectCommand,
            Guid>(
            new CreateProjectCommand(
                "Proyecto dos",
                "Folk",
                "Descripción dos",
                CreateMusicalDNA()));

        var result = await dispatcher.SendQueryAsync<
            ListMusicalProjectsQuery,
            ListMusicalProjectsResult>(
            new ListMusicalProjectsQuery());

        Assert.NotNull(result);
        Assert.Equal(2, result!.Projects.Count);
        Assert.Contains(
            result.Projects,
            project => project.Name == "Proyecto uno");
        Assert.Contains(
            result.Projects,
            project => project.Name == "Proyecto dos");
    }

    private static ServiceProvider CreateServiceProvider(
        SqliteTestDatabase database)
    {
        var services = new ServiceCollection();

        services.AddApplication();
        services.AddJELMusicFramework();

        services.AddScoped(_ => database.Context);

        services.AddScoped<
            IMusicalProjectRepository,
            MusicalProjectRepository>();

        services.AddScoped<
            IUnitOfWork,
            CoreUnitOfWork>();

        return services.BuildServiceProvider();
    }

    private static MusicalDNA CreateMusicalDNA()
    {
        return new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Neutral",
                120,
                "Instrumental"));
    }
}
