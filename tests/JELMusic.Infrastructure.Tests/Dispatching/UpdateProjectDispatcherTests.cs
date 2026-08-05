using JELMusic.Application;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Projects.UpdateProject;
using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Infrastructure;
using JELMusic.Infrastructure.Persistence;
using JELMusic.Infrastructure.Tests.TestData;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Infrastructure.Tests.Dispatching;

public class UpdateProjectDispatcherTests
{
    [Fact]
    public async Task Should_update_project_through_dispatcher()
    {
        var services = new ServiceCollection();

        services.AddApplication();

        var connection = new SqliteConnection(
            "Data Source=UpdateProjectTest;Mode=Memory;Cache=Shared");

        connection.Open();

        services.AddInfrastructure(connection.ConnectionString);

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var db = scope.ServiceProvider
            .GetRequiredService<CoreDbContext>();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var dispatcher = scope.ServiceProvider
            .GetRequiredService<IApplicationDispatcher>();

        var createCommand = new CreateProjectCommand(
            "Proyecto inicial",
            "Pop",
            "Descripción inicial",
            CreateProjectTestData.CreateDNA());

        var projectId =
            await dispatcher.SendCommandAsync<CreateProjectCommand, Guid>(
                createCommand);

        var updateCommand = new UpdateProjectCommand(
            projectId,
            "Proyecto actualizado",
            "Worship",
            "Descripción actualizada",
            CreateProjectTestData.CreateDNA());

        var result =
            await dispatcher.SendCommandAsync<
                UpdateProjectCommand,
                UpdateProjectResult>(updateCommand);

        Assert.Equal(projectId, result.ProjectId);
        Assert.True(result.ProjectVersion > 0);

        var query = new GetMusicalProjectByIdQuery(projectId);

        var project =
            await dispatcher.SendQueryAsync<
                GetMusicalProjectByIdQuery,
                GetMusicalProjectByIdResult>(query);

        Assert.NotNull(project);
        Assert.Equal("Proyecto actualizado", project!.Name);
        Assert.Equal("Worship", project.Genre);
        Assert.Equal("Descripción actualizada", project.Description);

        connection.Dispose();
    }
}
