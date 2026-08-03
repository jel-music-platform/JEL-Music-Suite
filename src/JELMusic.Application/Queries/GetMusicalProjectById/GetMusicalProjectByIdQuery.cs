using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed record GetMusicalProjectByIdQuery(Guid ProjectId)
    : IQuery<GetMusicalProjectByIdResult>;