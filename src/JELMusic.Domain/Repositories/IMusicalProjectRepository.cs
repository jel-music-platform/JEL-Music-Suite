using JELMusic.Domain.Entities;

namespace JELMusic.Domain.Repositories;

public interface IMusicalProjectRepository
{
    Task<MusicalProject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<MusicalProject>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        MusicalProject project,
        CancellationToken cancellationToken = default);

    void Update(MusicalProject project);

    void Remove(MusicalProject project);
}