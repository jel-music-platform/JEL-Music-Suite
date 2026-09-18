using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Tests.Fakes;

public sealed class FakeVideoProjectRepository
    : IVideoProjectRepository
{
    public List<VideoProject> Projects { get; } = new();

    public CancellationToken LastCancellationToken { get; private set; }

    public Task AddAsync(
        VideoProject project,
        CancellationToken cancellationToken = default)
    {
        LastCancellationToken = cancellationToken;
        Projects.Add(project);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<VideoProject>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        LastCancellationToken = cancellationToken;

        return Task.FromResult<IReadOnlyList<VideoProject>>(Projects);
    }

    public Task<VideoProject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            Projects.FirstOrDefault(p => p.Id == id));
    }

    public void Update(VideoProject project)
    {
    }

    public void Remove(VideoProject project)
    {
        Projects.Remove(project);
    }
}