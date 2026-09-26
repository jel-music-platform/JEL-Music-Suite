using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Tests.Fakes;

public sealed class FakeVideoSceneRepository
    : IVideoSceneRepository
{
    public List<VideoScene> Scenes { get; } = new();

    public CancellationToken LastCancellationToken { get; private set; }

    public Task AddAsync(
        VideoScene scene,
        CancellationToken cancellationToken = default)
    {
        LastCancellationToken = cancellationToken;
        Scenes.Add(scene);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<VideoScene>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        LastCancellationToken = cancellationToken;

        return Task.FromResult<IReadOnlyList<VideoScene>>(Scenes);
    }

    public Task<VideoScene?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        LastCancellationToken = cancellationToken;

        return Task.FromResult(
            Scenes.FirstOrDefault(scene => scene.Id == id));
    }

    public void Update(VideoScene scene)
    {
    }

    public void Remove(VideoScene scene)
    {
        Scenes.Remove(scene);
    }
}