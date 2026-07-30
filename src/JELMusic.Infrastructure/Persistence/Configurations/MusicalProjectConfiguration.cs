using JELMusic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JELMusic.Infrastructure.Persistence.Configurations;

public sealed class MusicalProjectConfiguration
    : IEntityTypeConfiguration<MusicalProject>
{
    public void Configure(EntityTypeBuilder<MusicalProject> entity)
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
    }
}