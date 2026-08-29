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

        Assert.Equal(1, factory.CreateCallCount);
        Assert.NotNull(factory.CreatedProject);
        Assert.Equal(command.Name, factory.CreatedProject!.Name);
        Assert.Equal(command.Genre, factory.CreatedProject.Genre);
        Assert.Equal(command.Description, factory.CreatedProject.Description);

        Assert.True(unitOfWork.Saved);

        Assert.Single(repository.Projects);

        var project = repository.Projects[0];

        Assert.Equal(result, project.Id);
        Assert.Equal("Proyecto prueba", project.Name);
        Assert.Equal("Pop", project.Genre);
        Assert.Equal("Descripción de prueba", project.Description);
    }

    [Fact]
    public async Task Should_propagate_cancellation_token()
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

        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        await handler.HandleAsync(command, cancellationToken);

        Assert.Equal(
            cancellationToken,
            repository.LastCancellationToken);

        Assert.Equal(
            cancellationToken,
            unitOfWork.LastCancellationToken);
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
