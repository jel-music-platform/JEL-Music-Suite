
using JELMusic.Application.VideoProjects.UpdateVideoSceneTiming;
using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;
using NSubstitute;

namespace JELMusic.Application.Tests.VideoProjects.UpdateVideoSceneTiming;

public class UpdateVideoSceneTimingCommandHandlerTests
{
    [Fact]
    public async Task Should_update_scene_timing_and_save_changes()
    {
        var projectRepository = Substitute.For<IVideoProjectRepository>();
        var sceneRepository = Substitute.For<IVideoSceneRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var videoProject = VideoProject.Create(
            Guid.NewGuid(),
            "Jig de Dublín",
            "Videoclip ambientado en una taberna irlandesa.",
            TimeSpan.FromMinutes(2) + TimeSpan.FromSeconds(40));

        var scene = VideoScene.Create(
            videoProject.Id,
            "Entrada a la taberna",
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(35),
            "Johnny entra en la taberna.");

        sceneRepository.GetByIdAsync(
                scene.Id,
                Arg.Any<CancellationToken>())
            .Returns(scene);

        projectRepository.GetByIdAsync(
                videoProject.Id,
                Arg.Any<CancellationToken>())
            .Returns(videoProject);

        var newStart = TimeSpan.FromSeconds(15);
        var newEnd = TimeSpan.FromSeconds(50);

        var command = new UpdateVideoSceneTimingCommand(
            scene.Id,
            newStart,
            newEnd);

        var handler = new UpdateVideoSceneTimingCommandHandler(
            projectRepository,
            sceneRepository,
            unitOfWork);

        var result = await handler.HandleAsync(command);

        Assert.Equal(scene.Id, result);
        Assert.Equal(newStart, scene.StartTime);
        Assert.Equal(newEnd, scene.EndTime);
        Assert.Equal(TimeSpan.FromSeconds(35), scene.Duration);

        sceneRepository.Received(1).Update(scene);

        await unitOfWork.Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_throw_when_scene_does_not_exist()
    {
        var projectRepository = Substitute.For<IVideoProjectRepository>();
        var sceneRepository = Substitute.For<IVideoSceneRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var sceneId = Guid.NewGuid();

        sceneRepository.GetByIdAsync(
                sceneId,
                Arg.Any<CancellationToken>())
            .Returns((VideoScene?)null);

        var handler = new UpdateVideoSceneTimingCommandHandler(
            projectRepository,
            sceneRepository,
            unitOfWork);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.HandleAsync(
                new UpdateVideoSceneTimingCommand(
                    sceneId,
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(30))));

        Assert.Equal("Video scene not found.", exception.Message);

        await unitOfWork.DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_throw_when_scene_end_exceeds_project_duration()
    {
        var projectRepository = Substitute.For<IVideoProjectRepository>();
        var sceneRepository = Substitute.For<IVideoSceneRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var videoProject = VideoProject.Create(
            Guid.NewGuid(),
            "Jig de Dublín",
            "Videoclip ambientado en una taberna irlandesa.",
            TimeSpan.FromMinutes(2) + TimeSpan.FromSeconds(40));

        var scene = VideoScene.Create(
            videoProject.Id,
            "Entrada a la taberna",
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(35),
            "Johnny entra en la taberna.");

        sceneRepository.GetByIdAsync(
                scene.Id,
                Arg.Any<CancellationToken>())
            .Returns(scene);

        projectRepository.GetByIdAsync(
                videoProject.Id,
                Arg.Any<CancellationToken>())
            .Returns(videoProject);

        var handler = new UpdateVideoSceneTimingCommandHandler(
            projectRepository,
            sceneRepository,
            unitOfWork);

        var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => handler.HandleAsync(
                new UpdateVideoSceneTimingCommand(
                    scene.Id,
                    TimeSpan.FromSeconds(120),
                    TimeSpan.FromMinutes(3))));

        Assert.Equal(
            nameof(UpdateVideoSceneTimingCommand.EndTime),
            exception.ParamName);

        sceneRepository.DidNotReceive().Update(Arg.Any<VideoScene>());

        await unitOfWork.DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_throw_when_command_is_null()
    {
        var projectRepository = Substitute.For<IVideoProjectRepository>();
        var sceneRepository = Substitute.For<IVideoSceneRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var handler = new UpdateVideoSceneTimingCommandHandler(
            projectRepository,
            sceneRepository,
            unitOfWork);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }
}
