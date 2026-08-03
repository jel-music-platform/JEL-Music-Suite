using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed class GetMusicalProjectByIdHandler
    : IQueryHandler<GetMusicalProjectByIdQuery, GetMusicalProjectByIdResult>
{
    private readonly IMusicalProjectRepository _repository;

    public GetMusicalProjectByIdHandler(
        IMusicalProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetMusicalProjectByIdResult?> HandleAsync(
        GetMusicalProjectByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var project = await _repository.GetByIdAsync(
            query.ProjectId,
            cancellationToken);

        if (project is null)
        {
            return null;
        }

        return new GetMusicalProjectByIdResult(
            project.Id,
            project.Name,
            project.Genre,
            project.Description);
    }
}