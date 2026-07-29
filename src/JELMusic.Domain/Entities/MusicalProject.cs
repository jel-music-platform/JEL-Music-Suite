using JELMusic.Domain.Enums;
using JELMusic.Domain.ValueObjects;

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

    public MusicalDNA? DNA { get; private set; }

    private MusicalProject()
    {
        Name = string.Empty;
        Genre = string.Empty;
        Description = string.Empty;
    }

    public MusicalProject(
        string name,
        string genre,
        string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Genre = genre;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        Status = ProjectStatus.Draft;
        ProjectVersion = 1;
    }
}