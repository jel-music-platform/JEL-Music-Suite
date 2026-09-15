using JELMusic.Application;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Queries.ListMusicalProjects;
using JELMusic.Infrastructure;
using JELMusic.Infrastructure.Persistence;
using JELMusic.Infrastructure.Tests.TestData;
using JELMusic.Framework;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Infrastructure.Tests.Dispatching;

public class ListMusicalProjectsDispatcherTests
{
    [Fact]
    public async Task Should_list_projects_through_dispatcher()
    {
        var services = new ServiceCollection();

        services.AddApplication();
        services.AddJELMusicFramework();

        var connection = new SqliteConnection(
            $"Data Source={Guid.NewGuid()};Mode=Memory;Cache=Shared");

        connection.Open();

        services.AddInfrastructure(
            connection.ConnectionString);

        using var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();

        var db = scope.ServiceProvider
            .GetRequiredService<CoreDbContext>();

        db.Database.EnsureCreated();

        var dispatcher = scope.ServiceProvider
            .GetRequiredService<IApplicationDispatcher>();

        var firstProjectId =
            await dispatcher.SendCommandAsync<CreateProjectCommand, Guid>(
                new CreateProjectCommand(
                    "Proyecto Lista 1",
                    "Worship",
                    "Primer proyecto",
                    CreateProjectTestData.CreateDNA()));

        var secondProjectId =
            await dispatcher.SendCommandAsync<CreateProjectCommand, Guid>(
                new CreateProjectCommand(
                    "Proyecto Lista 2",
                    "Folk",
                    "Segundo proyecto",
                    CreateProjectTestData.CreateDNA()));

        var query = new ListMusicalProjectsQuery();

        var result =
            await dispatcher.SendQueryAsync<
                ListMusicalProjectsQuery,
                ListMusicalProjectsResult>(
                    query);

        Assert.NotNull(result);
        Assert.Equal(2, result!.Projects.Count);

        Assert.Contains(
            result.Projects,
            project => project.ProjectId == firstProjectId);

        Assert.Contains(
            result.Projects,
            project => project.ProjectId == secondProjectId);

        Assert.Contains(
            result.Projects,
            project => project.Name == "Proyecto Lista 1");

        Assert.Contains(
            result.Projects,
            project => project.Name == "Proyecto Lista 2");

        connection.Dispose();
    }
}
