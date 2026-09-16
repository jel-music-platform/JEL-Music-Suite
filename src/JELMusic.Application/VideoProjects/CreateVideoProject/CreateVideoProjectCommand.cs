using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.VideoProjects.CreateVideoProject;

public sealed record CreateVideoProjectCommand(
    Guid MusicalProjectId,
    string Name,
    string Concept,
    TimeSpan Duration)
    : ICommand<Guid>;
