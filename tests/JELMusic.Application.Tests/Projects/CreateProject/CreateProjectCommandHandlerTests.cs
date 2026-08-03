using JELMusic.Application.Projects.CreateProject;

namespace JELMusic.Application.Tests.Projects.CreateProject;

public class CreateProjectCommandHandlerTests
{
    [Fact]
    public async Task Should_return_project_id_when_command_is_valid()
    {
        var handler = new CreateProjectCommandHandler();

        var command = new CreateProjectCommand(
            "Proyecto prueba",
            null!);

        var result = await handler.HandleAsync(command);

        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public async Task Should_throw_when_command_is_null()
    {
        var handler = new CreateProjectCommandHandler();

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }
}