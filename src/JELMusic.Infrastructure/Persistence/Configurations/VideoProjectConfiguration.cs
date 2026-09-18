using JELMusic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JELMusic.Infrastructure.Persistence.Configurations;

public sealed class VideoProjectConfiguration : IEntityTypeConfiguration<VideoProject>
{
    public void Configure(EntityTypeBuilder<VideoProject> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne<MusicalProject>()
            .WithMany()
            .HasForeignKey(p => p.MusicalProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.MusicalProjectId)
            .IsRequired();

        builder.Property(p => p.Name)
            .IsRequired();

        builder.Property(p => p.Concept)
            .IsRequired();

        builder.Property(p => p.Duration)
            .HasConversion(
                duration => duration.Ticks,
                ticks => TimeSpan.FromTicks(ticks))
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion<int>()
            .IsRequired();
    }
}