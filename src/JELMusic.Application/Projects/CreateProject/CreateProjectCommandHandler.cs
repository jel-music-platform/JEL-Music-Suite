using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Repositories;
using JELMusic.Domain.Services;

namespace JELMusic.Application.Projects.CreateProject;

public sealed class CreateProjectCommandHandler
    : ICommandHandler<CreateProjectCommand, Guid>
{
    private readonly IMusicalProjectRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMusicalProjectFactory _factory;

    public CreateProjectCommandHandler(
        IMusicalProjectRepository repository,
        IUnitOfWork unitOfWork,
        IMusicalProjectFactory factory)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _factory = factory;
    }

    public async Task<Guid> HandleAsync(
        CreateProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var project = _factory.Create(
            command.Name,
            command.Genre,
            command.Description,
            command.MusicalDNA);

        await _repository.AddAsync(
            project,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return project.Id;
    }
}