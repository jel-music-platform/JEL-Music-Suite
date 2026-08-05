using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Application.Tests.Projects.CreateProject;

public class CreateProjectCommandHandlerTests
{
    [Fact]
    public async Task Should_return_project_id_when_command_is_valid()
    {
        var repository = new FakeMusicalProjectRepository();
        var unitOfWork = new FakeUnitOfWork();
        var factory = new FakeMusicalProjectFactory();

        var handler = new CreateProjectCommandHandler(
            repository,
            unitOfWork,
            factory);

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
        Assert.True(unitOfWork.Saved);
    }

    [Fact]
    public async Task Should_throw_when_command_is_null()
    {
        var repository = new FakeMusicalProjectRepository();
        var unitOfWork = new FakeUnitOfWork();
        var factory = new FakeMusicalProjectFactory();

        var handler = new CreateProjectCommandHandler(
            repository,
            unitOfWork,
            factory);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }
}