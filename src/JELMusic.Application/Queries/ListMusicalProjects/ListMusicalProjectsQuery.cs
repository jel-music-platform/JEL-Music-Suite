using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.Queries.ListMusicalProjects;

public sealed record ListMusicalProjectsQuery
    : IQuery<ListMusicalProjectsResult>;
