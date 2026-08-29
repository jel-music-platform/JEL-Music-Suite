using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Tests.Fakes;

public sealed class FakeMusicalProjectRepository
    : IMusicalProjectRepository
{
    public List<MusicalProject> Projects { get; } = new();

    public CancellationToken LastCancellationToken { get; private set; }

    public Task AddAsync(
        MusicalProject project,
        CancellationToken cancellationToken = default)
    {
        LastCancellationToken = cancellationToken;
        Projects.Add(project);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<MusicalProject>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<MusicalProject>>(Projects);
    }

    public Task<MusicalProject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            Projects.FirstOrDefault(p => p.Id == id));
    }

    public void Update(MusicalProject project)
    {
    }

    public void Remove(MusicalProject project)
    {
        Projects.Remove(project);
    }
}
