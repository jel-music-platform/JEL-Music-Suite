namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed record GetMusicalProjectByIdResult(
    Guid ProjectId,
    string Name,
    string Genre,
    string Description);