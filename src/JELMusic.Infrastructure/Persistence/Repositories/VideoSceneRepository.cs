using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JELMusic.Infrastructure.Persistence.Repositories;

public sealed class VideoSceneRepository : IVideoSceneRepository
{
    private readonly CoreDbContext _context;

    public VideoSceneRepository(CoreDbContext context)
    {
        _context = context;
    }

    public async Task<VideoScene?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.VideoScenes
            .FirstOrDefaultAsync(
                scene => scene.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<VideoScene>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.VideoScenes
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        VideoScene scene,
        CancellationToken cancellationToken = default)
    {
        await _context.VideoScenes.AddAsync(
            scene,
            cancellationToken);
    }

    public void Update(VideoScene scene)
    {
        ArgumentNullException.ThrowIfNull(scene);

        _context.VideoScenes.Update(scene);
    }

    public void Remove(VideoScene scene)
    {
        _context.VideoScenes.Remove(scene);
    }
}
