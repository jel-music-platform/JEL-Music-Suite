using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.VideoProjects.CreateVideoScene;
using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;
using NSubstitute;

namespace JELMusic.Application.Tests.VideoProjects.CreateVideoScene;

public class CreateVideoSceneCommandHandlerTests
{
    [Fact]
    public async Task Should_return_video_scene_id_when_command_is_valid()
    {
        var projectRepository = Substitute.For<IVideoProjectRepository>();
        var sceneRepository = Substitute.For<IVideoSceneRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var videoProject = VideoProject.Create(
            Guid.NewGuid(),
            "Jig de Dublín",
            "Videoclip ambientado en una taberna irlandesa.",
            TimeSpan.FromMinutes(2) + TimeSpan.FromSeconds(40));

        projectRepository.GetByIdAsync(
                videoProject.Id,
                Arg.Any<CancellationToken>())
            .Returns(videoProject);

        var command = new CreateVideoSceneCommand(
            videoProject.Id,
            "Entrada a la taberna",
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(35),
            "Johnny entra en la taberna.");

        var handler = new CreateVideoSceneCommandHandler(
            projectRepository,
            sceneRepository,
            unitOfWork);

        var result = await handler.HandleAsync(command);

        Assert.NotEqual(Guid.Empty, result);

        await sceneRepository.Received(1)
            .AddAsync(
                Arg.Is<VideoScene>(scene =>
                    scene.Id == result &&
                    scene.VideoProjectId == videoProject.Id &&
                    scene.Name == "Entrada a la taberna" &&
                    scene.StartTime == TimeSpan.FromSeconds(10) &&
                    scene.EndTime == TimeSpan.FromSeconds(35) &&
                    scene.Description == "Johnny entra en la taberna."),
                Arg.Any<CancellationToken>());

        await unitOfWork.Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_throw_when_video_project_does_not_exist()
    {
        var projectRepository = Substitute.For<IVideoProjectRepository>();
        var sceneRepository = Substitute.For<IVideoSceneRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var videoProjectId = Guid.NewGuid();

        projectRepository.GetByIdAsync(
                videoProjectId,
                Arg.Any<CancellationToken>())
            .Returns((VideoProject?)null);

        var command = new CreateVideoSceneCommand(
            videoProjectId,
            "Entrada a la taberna",
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(35),
            "Johnny entra en la taberna.");

        var handler = new CreateVideoSceneCommandHandler(
            projectRepository,
            sceneRepository,
            unitOfWork);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.HandleAsync(command));

        Assert.Equal(
            "Video project not found.",
            exception.Message);

        await sceneRepository.DidNotReceive()
            .AddAsync(
                Arg.Any<VideoScene>(),
                Arg.Any<CancellationToken>());

        await unitOfWork.DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_throw_when_scene_end_time_exceeds_project_duration()
    {
        var projectRepository = Substitute.For<IVideoProjectRepository>();
        var sceneRepository = Substitute.For<IVideoSceneRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var videoProject = VideoProject.Create(
            Guid.NewGuid(),
            "Jig de Dublín",
            "Videoclip ambientado en una taberna irlandesa.",
            TimeSpan.FromMinutes(2) + TimeSpan.FromSeconds(40));

        projectRepository.GetByIdAsync(
                videoProject.Id,
                Arg.Any<CancellationToken>())
            .Returns(videoProject);

        var command = new CreateVideoSceneCommand(
            videoProject.Id,
            "Escena demasiado larga",
            TimeSpan.FromMinutes(2),
            TimeSpan.FromMinutes(3),
            "La escena supera la duración del proyecto.");

        var handler = new CreateVideoSceneCommandHandler(
            projectRepository,
            sceneRepository,
            unitOfWork);

        var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => handler.HandleAsync(command));

        Assert.Equal(
            nameof(CreateVideoSceneCommand.EndTime),
            exception.ParamName);

        await sceneRepository.DidNotReceive()
            .AddAsync(
                Arg.Any<VideoScene>(),
                Arg.Any<CancellationToken>());

        await unitOfWork.DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_throw_when_command_is_null()
    {
        var projectRepository = Substitute.For<IVideoProjectRepository>();
        var sceneRepository = Substitute.For<IVideoSceneRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var handler = new CreateVideoSceneCommandHandler(
            projectRepository,
            sceneRepository,
            unitOfWork);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));

        await projectRepository.DidNotReceive()
            .GetByIdAsync(
                Arg.Any<Guid>(),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_propagate_cancellation_token()
    {
        var projectRepository = Substitute.For<IVideoProjectRepository>();
        var sceneRepository = Substitute.For<IVideoSceneRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var videoProject = VideoProject.Create(
            Guid.NewGuid(),
            "Jig de Dublín",
            "Videoclip ambientado en una taberna irlandesa.",
            TimeSpan.FromMinutes(2) + TimeSpan.FromSeconds(40));

        projectRepository.GetByIdAsync(
                videoProject.Id,
                Arg.Any<CancellationToken>())
            .Returns(videoProject);

        var command = new CreateVideoSceneCommand(
            videoProject.Id,
            "Entrada a la taberna",
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(35),
            "Johnny entra en la taberna.");

        using var cancellationTokenSource =
            new CancellationTokenSource();

        var cancellationToken = cancellationTokenSource.Token;

        var handler = new CreateVideoSceneCommandHandler(
            projectRepository,
            sceneRepository,
            unitOfWork);

        await handler.HandleAsync(
            command,
            cancellationToken);

        await projectRepository.Received(1)
            .GetByIdAsync(
                videoProject.Id,
                cancellationToken);

        await sceneRepository.Received(1)
            .AddAsync(
                Arg.Any<VideoScene>(),
                cancellationToken);

        await unitOfWork.Received(1)
            .SaveChangesAsync(cancellationToken);
    }
}
