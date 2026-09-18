using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Queries.ListVideoProjects;

public sealed class ListVideoProjectsHandler
    : IQueryHandler<ListVideoProjectsQuery, ListVideoProjectsResult>
{
    private readonly IVideoProjectRepository _repository;

    public ListVideoProjectsHandler(
        IVideoProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListVideoProjectsResult?> HandleAsync(
        ListVideoProjectsQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var projects = await _repository.GetAllAsync(
            cancellationToken);

        var items = projects
            .Select(project => new VideoProjectListItem(
                project.Id,
                project.MusicalProjectId,
                project.Name,
                project.Concept,
                project.Duration,
                project.CreatedAt,
                project.Status))
            .ToList();

        return new ListVideoProjectsResult(items);
    }
}
