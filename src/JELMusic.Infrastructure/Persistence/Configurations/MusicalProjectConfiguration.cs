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
           dna.OwnsMany(x => x.InfluenceProfiles, influence =>
{
    influence.WithOwner();

    influence.Property<int>("Id");

    influence.HasKey("Id");

    influence.Property(x => x.Name)
        .HasColumnName("Name")
        .HasMaxLength(200);

    influence.Property(x => x.Description)
        .HasColumnName("Description")
        .HasMaxLength(1000);

    influence.Property(x => x.CulturalContext)
        .HasColumnName("CulturalContext")
        .HasMaxLength(500);

    influence.Property(x => x.InfluenceType)
        .HasColumnName("InfluenceType")
        .HasMaxLength(200);

    influence.Property(x => x.MusicalContribution)
        .HasColumnName("MusicalContribution")
        .HasMaxLength(1000);

    influence.OwnsOne(x => x.Origin, origin =>
    {
        origin.Property(x => x.Source)
            .HasColumnName("OriginSource")
            .HasMaxLength(200);

        origin.Property(x => x.Reference)
            .HasColumnName("OriginReference")
            .HasMaxLength(500);

        origin.Property(x => x.CulturalContext)
            .HasColumnName("OriginCulturalContext")
            .HasMaxLength(500);

        origin.Property(x => x.RegisteredAt)
            .HasColumnName("OriginRegisteredAt");
    });
});
            dna.OwnsMany(x => x.InstrumentProfiles, instrument =>
{
    instrument.WithOwner();

    instrument.Property("Id");
    instrument.HasKey("Id");

    instrument.Property(x => x.Name)
        .HasColumnName("Name")
        .HasMaxLength(200);

    instrument.Property(x => x.Description)
        .HasColumnName("Description")
        .HasMaxLength(1000);

    instrument.Property(x => x.CulturalContext)
        .HasColumnName("CulturalContext")
        .HasMaxLength(500);

    instrument.Property(x => x.Family)
        .HasColumnName("Family")
        .HasMaxLength(200);

    instrument.Property(x => x.Function)
    .HasColumnName("Function")
    .HasMaxLength(500);

    instrument.Property(x => x.Character)
    .HasColumnName("Character")
    .HasMaxLength(200);
    instrument.OwnsOne(x => x.Origin, origin =>
    {
        origin.Property(x => x.Source)
            .HasColumnName("OriginSource")
            .HasMaxLength(200);

        origin.Property(x => x.Reference)
            .HasColumnName("OriginReference")
            .HasMaxLength(500);

        origin.Property(x => x.CulturalContext)
            .HasColumnName("OriginCulturalContext")
            .HasMaxLength(500);

        origin.Property(x => x.RegisteredAt)
            .HasColumnName("OriginRegisteredAt");
    });
});

            dna.OwnsOne(x => x.Style, style =>
            {
                style.Property(x => x.Name)
                    .HasColumnName("StyleName")
                    .HasMaxLength(200);

                style.Property(x => x.Description)
                    .HasColumnName("StyleDescription")
                    .HasMaxLength(1000);

                style.Property(x => x.CulturalContext)
                    .HasColumnName("StyleCulturalContext")
                    .HasMaxLength(500);

                style.Property(x => x.Characteristics)
                    .HasColumnName("StyleCharacteristics")
                    .HasMaxLength(500);

                style.Property(x => x.Character)
                    .HasColumnName("StyleCharacter")
                    .HasMaxLength(200);

                style.OwnsOne(x => x.Origin, origin =>
                {
                    origin.Property(x => x.Source)
                        .HasColumnName("StyleOriginSource")
                        .HasMaxLength(200);

                    origin.Property(x => x.Reference)
                        .HasColumnName("StyleOriginReference")
                        .HasMaxLength(500);

                    origin.Property(x => x.CulturalContext)
                        .HasColumnName("StyleOriginCulturalContext")
                        .HasMaxLength(500);

                    origin.Property(x => x.RegisteredAt)
                        .HasColumnName("StyleOriginRegisteredAt");
                });
            });

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