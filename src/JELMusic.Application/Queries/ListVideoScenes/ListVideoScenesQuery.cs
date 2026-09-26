using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.Queries.ListVideoScenes;

public sealed record ListVideoScenesQuery(
    Guid VideoProjectId)
    : IQuery<ListVideoScenesResult>;
