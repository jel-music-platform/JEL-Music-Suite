namespace JELMusic.Application.Queries.ListMusicalProjects;

public sealed record ListMusicalProjectsResult(
    IReadOnlyList<MusicalProjectListItem> Projects);

public sealed record MusicalProjectListItem(
    Guid ProjectId,
    string Name,
    string Genre,
    string Description);
