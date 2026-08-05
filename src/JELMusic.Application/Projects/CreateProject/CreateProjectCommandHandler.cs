using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Projects.CreateProject;

public sealed class CreateProjectCommandHandler
    : ICommandHandler<CreateProjectCommand, Guid>
{
    private readonly IMusicalProjectRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(
        IMusicalProjectRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> HandleAsync(
        CreateProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var project = MusicalProject.Create(
            command.Name,
            command.Genre,
            command.Description,
            command.MusicalDNA);

        await _repository.AddAsync(project, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}