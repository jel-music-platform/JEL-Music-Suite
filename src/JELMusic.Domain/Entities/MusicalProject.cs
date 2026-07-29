using JELMusic.Domain.Enums;

namespace JELMusic.Domain.Entities;

public class MusicalProject
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Genre { get; private set; }

    public string Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public ProjectStatus Status { get; private set; }

    public int Version { get; private set; }

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
        Version = 1;
    }
}