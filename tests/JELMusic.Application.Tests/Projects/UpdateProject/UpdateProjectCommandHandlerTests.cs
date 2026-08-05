using JELMusic.Application.Projects.UpdateProject;
using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;
using JELMusic.Domain.Services;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;
using NSubstitute;

namespace JELMusic.Application.Tests.Projects.UpdateProject;

public class UpdateProjectCommandHandlerTests
{
    [Fact]
    public async Task Should_update_existing_project()
    {
        var repository = Substitute.For<IMusicalProjectRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var factory = new MusicalProjectFactory();

        var dna = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Piano",
                90,
                "Soft"));

        var project = factory.Create(
            "Old name",
            "Pop",
            "Old description",
            dna);

        repository.GetByIdAsync(
            project.Id,
            Arg.Any<CancellationToken>())
            .Returns(project);

        var handler = new UpdateProjectCommandHandler(
            repository,
            unitOfWork);

        var command = new UpdateProjectCommand(
            project.Id,
            "New name",
            "Worship",
            "New description",
            dna);

        var result = await handler.HandleAsync(command);

        Assert.Equal(project.Id, result.ProjectId);
        Assert.Equal(2, result.ProjectVersion);
        Assert.Equal("New name", project.Name);
        Assert.Equal("Worship", project.Genre);
        Assert.Equal("New description", project.Description);
        Assert.Equal(2, project.ProjectVersion);

        await unitOfWork.Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}