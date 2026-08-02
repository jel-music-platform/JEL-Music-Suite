using JELMusic.Domain.Repositories;

namespace JELMusic.Infrastructure.Persistence;

public sealed class CoreUnitOfWork : IUnitOfWork
{
    private readonly CoreDbContext _context;

    public CoreUnitOfWork(CoreDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}