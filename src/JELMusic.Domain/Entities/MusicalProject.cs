using JELMusic.Domain.Enums;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Domain.Entities;

public class MusicalProject
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Genre { get; private set; }

    public string Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public ProjectStatus Status { get; private set; }

    public int ProjectVersion { get; private set; }

    public MusicalDNA DNA { get; private set; }

    private MusicalProject()
    {
        Name = string.Empty;
        Genre = string.Empty;
        Description = string.Empty;

        DNA = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                string.Empty,
                60,
                string.Empty));

        CreatedAt = DateTime.UtcNow;
        Status = ProjectStatus.Draft;
        ProjectVersion = 1;
    }

    public MusicalProject(
        string name,
        string genre,
        string description,
        MusicalDNA dna)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Project name cannot be empty.",
                nameof(name));

        if (string.IsNullOrWhiteSpace(genre))
            throw new ArgumentException(
                "Project genre cannot be empty.",
                nameof(genre));

        ArgumentNullException.ThrowIfNull(dna);

        Id = Guid.NewGuid();
        Name = name;
        Genre = genre;
        Description = description;
        DNA = dna;
        CreatedAt = DateTime.UtcNow;
        Status = ProjectStatus.Draft;
        ProjectVersion = 1;
    }
}