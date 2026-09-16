using JELMusic.Domain.Enums;

namespace JELMusic.Domain.Entities;

public class VideoProject
{
    public Guid Id { get; private set; }

    public Guid MusicalProjectId { get; private set; }

    public string Name { get; private set; }

    public string Concept { get; private set; }

    public TimeSpan Duration { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public VideoProjectStatus Status { get; private set; }

    private VideoProject()
    {
        Name = string.Empty;
        Concept = string.Empty;
        CreatedAt = DateTime.UtcNow;
        Status = VideoProjectStatus.Draft;
    }

    private VideoProject(
        Guid musicalProjectId,
        string name,
        string concept,
        TimeSpan duration)
    {
        if (musicalProjectId == Guid.Empty)
            throw new ArgumentException(
                "Musical project id cannot be empty.",
                nameof(musicalProjectId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Video project name cannot be empty.",
                nameof(name));

        if (duration < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(
                nameof(duration),
                "Video project duration cannot be negative.");

        Id = Guid.NewGuid();
        MusicalProjectId = musicalProjectId;
        Name = name;
        Concept = concept;
        Duration = duration;
        CreatedAt = DateTime.UtcNow;
        Status = VideoProjectStatus.Draft;
    }

    public static VideoProject Create(
        Guid musicalProjectId,
        string name,
        string concept,
        TimeSpan duration)
    {
        return new VideoProject(
            musicalProjectId,
            name,
            concept,
            duration);
    }
}
