using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.VideoProjects.UpdateVideoScene;

public sealed record UpdateVideoSceneCommand(
    Guid VideoSceneId,
    string Name,
    string Description,
    TimeSpan StartTime,
    TimeSpan EndTime) : ICommand<Guid>;