using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JELMusic.Infrastructure.Persistence.Repositories;

public sealed class VideoProjectRepository : IVideoProjectRepository
{
    private readonly CoreDbContext _context;

    public VideoProjectRepository(CoreDbContext context)
    {
        _context = context;
    }

    public async Task<VideoProject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.VideoProjects
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<VideoProject>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.VideoProjects
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        VideoProject project,
        CancellationToken cancellationToken = default)
    {
        await _context.VideoProjects.AddAsync(project, cancellationToken);
    }

    public void Update(VideoProject project)
    {
        ArgumentNullException.ThrowIfNull(project);

        _context.VideoProjects.Update(project);
    }

    public void Remove(VideoProject project)
    {
        _context.VideoProjects.Remove(project);
    }
}
