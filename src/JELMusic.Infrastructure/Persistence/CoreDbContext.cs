using JELMusic.Domain.Entities;
using JELMusic.Domain.ValueObjects;
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

        modelBuilder.Entity<MusicalProject>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Genre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Description)
                .HasMaxLength(2000);

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            entity.Property(x => x.Status)
                .IsRequired();

            entity.Property(x => x.ProjectVersion)
                .IsRequired();

           entity.Ignore(x => x.DNA);
        });
    }
}