using JELMusic.Application.Projects.CreateProject;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Application.Tests.Projects.CreateProject;

public class CreateProjectCommandHandlerTests
{
    [Fact]
    public async Task Should_return_project_id_when_command_is_valid()
    {
        var handler = new CreateProjectCommandHandler();

        var musicalDNA = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Neutral",
                120,
                "Instrumental"));

        var command = new CreateProjectCommand(
            "Proyecto prueba",
            "Pop",
            "Descripción de prueba",
            musicalDNA);

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