using JELMusic.Domain.Entities;

namespace JELMusic.Domain.Repositories;

public interface IVideoProjectRepository
{
    Task<VideoProject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<VideoProject>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        VideoProject project,
        CancellationToken cancellationToken = default);

    void Update(VideoProject project);

    void Remove(VideoProject project);
}
