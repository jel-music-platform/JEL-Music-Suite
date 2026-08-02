using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;
using JELMusic.Domain.ValueObjects;

namespace JELMusic.Application.UseCases.CreateMusicalProject;

public sealed class CreateMusicalProjectUseCase
{
    private readonly IMusicalProjectRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMusicalProjectUseCase(
        IMusicalProjectRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateMusicalProjectResult> ExecuteAsync(
        CreateMusicalProjectRequest request,
        MusicalDNA dna,
        CancellationToken cancellationToken = default)
    {
        var project = new MusicalProject(
            request.Name,
            request.Genre,
            request.Description,
            dna);

        await _repository.AddAsync(project, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateMusicalProjectResult(project.Id);
    }
}