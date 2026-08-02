namespace JELMusic.Application.UseCases.CreateMusicalProject;

public sealed record CreateMusicalProjectRequest(
    string Name,
    string Genre,
    string Description);