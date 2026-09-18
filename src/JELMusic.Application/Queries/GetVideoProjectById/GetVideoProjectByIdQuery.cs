using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.Queries.GetVideoProjectById;

public sealed record GetVideoProjectByIdQuery(
    Guid VideoProjectId)
    : IQuery<GetVideoProjectByIdResult>;
