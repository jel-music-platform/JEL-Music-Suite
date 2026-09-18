using JELMusic.Domain.Enums;

namespace JELMusic.Application.Queries.ListVideoProjects;

public sealed record ListVideoProjectsResult(
    IReadOnlyList<VideoProjectListItem> Projects);

public sealed record VideoProjectListItem(
    Guid VideoProjectId,
    Guid MusicalProjectId,
    string Name,
    string Concept,
    TimeSpan Duration,
    DateTime CreatedAt,
    VideoProjectStatus Status);
