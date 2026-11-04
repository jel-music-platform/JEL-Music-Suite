using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.VideoProjects.UpdateVideoScene;

public sealed class UpdateVideoSceneCommandHandler
    : ICommandHandler<UpdateVideoSceneCommand, Guid>
{
    private readonly IVideoProjectRepository _videoProjectRepository;
    private readonly IVideoSceneRepository _videoSceneRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVideoSceneCommandHandler(
        IVideoProjectRepository videoProjectRepository,
        IVideoSceneRepository videoSceneRepository,
        IUnitOfWork unitOfWork)
    {
        _videoProjectRepository = videoProjectRepository;
        _videoSceneRepository = videoSceneRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> HandleAsync(
        UpdateVideoSceneCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var scene = await _videoSceneRepository.GetByIdAsync(
            command.VideoSceneId,
            cancellationToken);

        if (scene is null)
            throw new InvalidOperationException("Video scene not found.");

        var videoProject = await _videoProjectRepository.GetByIdAsync(
            scene.VideoProjectId,
            cancellationToken);

        if (videoProject is null)
            throw new InvalidOperationException("Video project not found.");

        if (command.EndTime > videoProject.Duration)
            throw new ArgumentOutOfRangeException(
                nameof(command.EndTime),
                "Video scene end time cannot exceed video project duration.");

        scene.UpdateDetails(
            command.Name,
            command.Description);

        scene.UpdateTiming(
            command.StartTime,
            command.EndTime);

        _videoSceneRepository.Update(scene);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return scene.Id;
    }
}