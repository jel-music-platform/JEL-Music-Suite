namespace JELMusic.Application.Queries.ListVideoScenes;

public sealed record ListVideoScenesResult(
    IReadOnlyList<VideoSceneListItem> Scenes);

public sealed record VideoSceneListItem(
    Guid VideoSceneId,
    Guid VideoProjectId,
    string Name,
    TimeSpan StartTime,
    TimeSpan EndTime,
    TimeSpan Duration,
    string Description);
