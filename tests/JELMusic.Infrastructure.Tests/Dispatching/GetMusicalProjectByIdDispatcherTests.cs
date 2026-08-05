using JELMusic.Application;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Infrastructure;
using JELMusic.Infrastructure.Persistence;
using JELMusic.Infrastructure.Tests.TestData;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Infrastructure.Tests.Dispatching;

public class GetMusicalProjectByIdDispatcherTests
{
    [Fact]
    public async Task Should_get_project_by_id_through_dispatcher()
    {
        var services = new ServiceCollection();

        services.AddApplication();

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

        var createCommand = new CreateProjectCommand(
            "Proyecto Query",
            "Worship",
            "Prueba Query Dispatcher",
            CreateProjectTestData.CreateDNA());

        var projectId =
            await dispatcher.SendCommandAsync<CreateProjectCommand, Guid>(
                createCommand);

        var query = new GetMusicalProjectByIdQuery(projectId);

        var result =
            await dispatcher.SendQueryAsync<
                GetMusicalProjectByIdQuery,
                GetMusicalProjectByIdResult>(
                    query);

        Assert.NotNull(result);
        Assert.Equal(projectId, result!.ProjectId);
        Assert.Equal("Proyecto Query", result.Name);
        Assert.Equal("Worship", result.Genre);

        connection.Dispose();
    }
}