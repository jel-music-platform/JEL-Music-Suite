using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.VideoProjects.CreateVideoProject;

public sealed class CreateVideoProjectCommandHandler
    : ICommandHandler<CreateVideoProjectCommand, Guid>
{
    private readonly IVideoProjectRepository _videoProjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVideoProjectCommandHandler(
        IVideoProjectRepository videoProjectRepository,
        IUnitOfWork unitOfWork)
    {
        _videoProjectRepository = videoProjectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> HandleAsync(
        CreateVideoProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var videoProject = VideoProject.Create(
            command.MusicalProjectId,
            command.Name,
            command.Concept,
            command.Duration);

        await _videoProjectRepository.AddAsync(
            videoProject,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return videoProject.Id;
    }
}
