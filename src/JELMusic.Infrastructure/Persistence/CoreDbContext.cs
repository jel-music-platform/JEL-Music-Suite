using JELMusic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JELMusic.Infrastructure.Persistence;

public sealed class CoreDbContext : DbContext
{
    public DbSet<MusicalProject> MusicalProjects => Set<MusicalProject>();

    public CoreDbContext(DbContextOptions<CoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreDbContext).Assembly);
    }
}