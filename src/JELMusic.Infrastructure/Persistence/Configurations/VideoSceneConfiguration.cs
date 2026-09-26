using JELMusic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JELMusic.Infrastructure.Persistence.Configurations;

public sealed class VideoSceneConfiguration : IEntityTypeConfiguration<VideoScene>
{
    public void Configure(EntityTypeBuilder<VideoScene> builder)
    {
        builder.HasKey(scene => scene.Id);

        builder.HasOne<VideoProject>()
            .WithMany()
            .HasForeignKey(scene => scene.VideoProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(scene => scene.VideoProjectId)
            .IsRequired();

        builder.Property(scene => scene.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(scene => scene.StartTime)
            .HasConversion(
                time => time.Ticks,
                ticks => TimeSpan.FromTicks(ticks))
            .IsRequired();

        builder.Property(scene => scene.EndTime)
            .HasConversion(
                time => time.Ticks,
                ticks => TimeSpan.FromTicks(ticks))
            .IsRequired();

        builder.Property(scene => scene.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Ignore(scene => scene.Duration);
    }
}
