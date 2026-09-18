using JELMusic.Domain.Enums;

namespace JELMusic.Application.Queries.GetVideoProjectById;

public sealed record GetVideoProjectByIdResult(
    Guid VideoProjectId,
    Guid MusicalProjectId,
    string Name,
    string Concept,
    TimeSpan Duration,
    DateTime CreatedAt,
    VideoProjectStatus Status);
