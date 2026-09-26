using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.VideoProjects.CreateVideoScene;

public sealed class CreateVideoSceneCommandHandler
    : ICommandHandler<CreateVideoSceneCommand, Guid>
{
    private readonly IVideoProjectRepository _videoProjectRepository;
    private readonly IVideoSceneRepository _videoSceneRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVideoSceneCommandHandler(
        IVideoProjectRepository videoProjectRepository,
        IVideoSceneRepository videoSceneRepository,
        IUnitOfWork unitOfWork)
    {
        _videoProjectRepository = videoProjectRepository;
        _videoSceneRepository = videoSceneRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> HandleAsync(
        CreateVideoSceneCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var videoProject = await _videoProjectRepository.GetByIdAsync(
            command.VideoProjectId,
            cancellationToken);

        if (videoProject is null)
            throw new InvalidOperationException(
                "Video project not found.");

        if (command.EndTime > videoProject.Duration)
            throw new ArgumentOutOfRangeException(
                nameof(command.EndTime),
                "Video scene end time cannot exceed video project duration.");

        var scene = VideoScene.Create(
            command.VideoProjectId,
            command.Name,
            command.StartTime,
            command.EndTime,
            command.Description);

        await _videoSceneRepository.AddAsync(
            scene,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return scene.Id;
    }
}
