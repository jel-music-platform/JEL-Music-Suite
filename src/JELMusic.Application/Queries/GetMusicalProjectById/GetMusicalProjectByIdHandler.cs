using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed class GetMusicalProjectByIdHandler
{
    private readonly IMusicalProjectRepository _repository;

    public GetMusicalProjectByIdHandler(
        IMusicalProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetMusicalProjectByIdResult?> ExecuteAsync(
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