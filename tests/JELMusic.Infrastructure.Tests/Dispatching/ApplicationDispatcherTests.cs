using JELMusic.Application;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Infrastructure;
using JELMusic.Infrastructure.Persistence;
using JELMusic.Infrastructure.Tests.TestData;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Infrastructure.Tests.Dispatching;

public class ApplicationDispatcherTests
{
    [Fact]
    public async Task Should_execute_command_through_dispatcher()
    {
        var services = new ServiceCollection();

        services.AddApplication();

        var connection = new SqliteConnection(
    "Data Source=JELTest;Mode=Memory;Cache=Shared");

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

        var command = new CreateProjectCommand(
            "Proyecto Dispatcher",
            "Worship",
            "Prueba dispatcher",
            CreateProjectTestData.CreateDNA());

        var result = await dispatcher.SendCommandAsync<CreateProjectCommand, Guid>(
            command);

        Assert.NotEqual(Guid.Empty, result);

        connection.Dispose();
    }
}