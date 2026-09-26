namespace JELMusic.Domain.Entities;

public class VideoScene
{
    public Guid Id { get; private set; }

    public Guid VideoProjectId { get; private set; }

    public string Name { get; private set; }

    public TimeSpan StartTime { get; private set; }

    public TimeSpan EndTime { get; private set; }

    public string Description { get; private set; }

    public TimeSpan Duration => EndTime - StartTime;

    private VideoScene()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    private VideoScene(
        Guid videoProjectId,
        string name,
        TimeSpan startTime,
        TimeSpan endTime,
        string description)
    {
        if (videoProjectId == Guid.Empty)
            throw new ArgumentException(
                "Video project id cannot be empty.",
                nameof(videoProjectId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Video scene name cannot be empty.",
                nameof(name));

        if (startTime < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(
                nameof(startTime),
                "Video scene start time cannot be negative.");

        if (endTime <= startTime)
            throw new ArgumentOutOfRangeException(
                nameof(endTime),
                "Video scene end time must be greater than start time.");

        Id = Guid.NewGuid();
        VideoProjectId = videoProjectId;
        Name = name;
        StartTime = startTime;
        EndTime = endTime;
        Description = description;
    }

    public static VideoScene Create(
        Guid videoProjectId,
        string name,
        TimeSpan startTime,
        TimeSpan endTime,
        string description)
    {
        return new VideoScene(
            videoProjectId,
            name,
            startTime,
            endTime,
            description);
    }
}
