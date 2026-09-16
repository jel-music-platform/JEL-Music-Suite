using JELMusic.Application.VideoProjects.CreateVideoProject;

namespace JELMusic.Application.Tests.VideoProjects.CreateVideoProject;

public class CreateVideoProjectCommandHandlerTests
{
    [Fact]
    public async Task Should_return_video_project_id_when_command_is_valid()
    {
        var musicalProjectId = Guid.NewGuid();

        var command = new CreateVideoProjectCommand(
            musicalProjectId,
            "Donde Está Tu Corazón",
            "Un hombre descubre que el éxito material no le proporciona satisfacción.",
            TimeSpan.FromMinutes(4) + TimeSpan.FromSeconds(28));

        var handler = new CreateVideoProjectCommandHandler();

        var result = await handler.HandleAsync(command);

        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public async Task Should_throw_when_command_is_null()
    {
        var handler = new CreateVideoProjectCommandHandler();

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }

}
