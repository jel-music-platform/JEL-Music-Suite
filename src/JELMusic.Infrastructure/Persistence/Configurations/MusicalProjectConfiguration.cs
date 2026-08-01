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

        entity.OwnsOne(x => x.DNA, dna =>
        {
            dna.Ignore(x => x.InfluenceProfiles);
            dna.Ignore(x => x.InstrumentProfiles);
            dna.Ignore(x => x.Style);

            dna.OwnsOne(x => x.Performance, performance =>
            {
                performance.Property(x => x.Mood)
                    .HasColumnName("Mood")
                    .HasMaxLength(100);

                performance.Property(x => x.TempoBpm)
                    .HasColumnName("TempoBpm");

                performance.Property(x => x.VocalStyle)
                    .HasColumnName("VocalStyle")
                    .HasMaxLength(100);
            });
        });
    }
}