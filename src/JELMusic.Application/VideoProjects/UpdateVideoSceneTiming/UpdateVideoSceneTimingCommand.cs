using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.VideoProjects.UpdateVideoSceneTiming;

public sealed record UpdateVideoSceneTimingCommand(
    Guid VideoSceneId,
    TimeSpan StartTime,
    TimeSpan EndTime) : ICommand<Guid>;