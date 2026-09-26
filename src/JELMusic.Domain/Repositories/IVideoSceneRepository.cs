using JELMusic.Domain.Entities;

namespace JELMusic.Domain.Repositories;

public interface IVideoSceneRepository
{
    Task<VideoScene?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<VideoScene>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        VideoScene scene,
        CancellationToken cancellationToken = default);

    void Update(VideoScene scene);

    void Remove(VideoScene scene);
}
