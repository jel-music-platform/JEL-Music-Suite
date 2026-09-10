using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Queries.ListMusicalProjects;

public sealed class ListMusicalProjectsHandler
    : IQueryHandler<ListMusicalProjectsQuery, ListMusicalProjectsResult>
{
    private readonly IMusicalProjectRepository _repository;

    public ListMusicalProjectsHandler(
        IMusicalProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListMusicalProjectsResult?> HandleAsync(
        ListMusicalProjectsQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var projects = await _repository.GetAllAsync(
            cancellationToken);

        var items = projects
            .Select(project => new MusicalProjectListItem(
                project.Id,
                project.Name,
                project.Genre,
                project.Description))
            .ToList();

        return new ListMusicalProjectsResult(items);
    }
}
