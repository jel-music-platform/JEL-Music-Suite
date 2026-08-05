using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Projects.UpdateProject;

public sealed class UpdateProjectCommandHandler
    : ICommandHandler<UpdateProjectCommand, UpdateProjectResult>
{
    private readonly IMusicalProjectRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectCommandHandler(
        IMusicalProjectRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateProjectResult> HandleAsync(
        UpdateProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var project = await _repository.GetByIdAsync(
            command.ProjectId,
            cancellationToken);

        if (project is null)
        {
            throw new InvalidOperationException(
                $"Project '{command.ProjectId}' was not found.");
        }

        project.Update(
            command.Name,
            command.Genre,
            command.Description,
            command.MusicalDNA);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new UpdateProjectResult(
            project.Id,
            project.ProjectVersion);
    }
}