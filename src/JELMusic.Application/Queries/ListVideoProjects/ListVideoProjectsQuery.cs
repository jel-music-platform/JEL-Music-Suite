using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.Queries.ListVideoProjects;

public sealed record ListVideoProjectsQuery
    : IQuery<ListVideoProjectsResult>;
