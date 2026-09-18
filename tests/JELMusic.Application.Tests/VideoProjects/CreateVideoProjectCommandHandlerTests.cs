using JELMusic.Application.VideoProjects.CreateVideoProject;
using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;
using NSubstitute;

namespace JELMusic.Application.Tests.VideoProjects.CreateVideoProject;

public class CreateVideoProjectCommandHandlerTests
{
    [Fact]
    public async Task Should_return_video_project_id_when_command_is_valid()
    {
        var repository = Substitute.For<IVideoProjectRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var musicalProjectId = Guid.NewGuid();

        var command = new CreateVideoProjectCommand(
            musicalProjectId,
            "Donde Está Tu Corazón",
            "Un hombre descubre que el éxito material no le proporciona satisfacción.",
            TimeSpan.FromMinutes(4) + TimeSpan.FromSeconds(28));

        var handler = new CreateVideoProjectCommandHandler(
            repository,
            unitOfWork);

        var result = await handler.HandleAsync(command);

        Assert.NotEqual(Guid.Empty, result);

        await repository.Received(1)
            .AddAsync(
                Arg.Is<VideoProject>(project =>
                    project.Id == result &&
                    project.MusicalProjectId == command.MusicalProjectId &&
                    project.Name == command.Name &&
                    project.Concept == command.Concept &&
                    project.Duration == command.Duration),
                Arg.Any<CancellationToken>());

        await unitOfWork.Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_throw_when_command_is_null()
    {
        var repository = Substitute.For<IVideoProjectRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var handler = new CreateVideoProjectCommandHandler(
            repository,
            unitOfWork);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }
}