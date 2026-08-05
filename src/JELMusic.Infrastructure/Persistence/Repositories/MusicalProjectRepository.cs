using JELMusic.Domain.Entities;
using JELMusic.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JELMusic.Infrastructure.Persistence.Repositories;

public sealed class MusicalProjectRepository : IMusicalProjectRepository
{
    private readonly CoreDbContext _context;

    public MusicalProjectRepository(CoreDbContext context)
    {
        _context = context;
    }

    public async Task<MusicalProject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.MusicalProjects
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MusicalProject>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.MusicalProjects
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        MusicalProject project,
        CancellationToken cancellationToken = default)
    {
        await _context.MusicalProjects.AddAsync(project, cancellationToken);
    }

    public void Update(MusicalProject project)
{
        ArgumentNullException.ThrowIfNull(project);

        _context.MusicalProjects.Update(project);
}

    public void Remove(MusicalProject project)
    {
        _context.MusicalProjects.Remove(project);
    }
}