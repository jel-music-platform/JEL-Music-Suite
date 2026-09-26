using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.VideoProjects.CreateVideoScene;

public sealed record CreateVideoSceneCommand(
    Guid VideoProjectId,
    string Name,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string Description)
    : ICommand<Guid>;
