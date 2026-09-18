using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Queries.GetVideoProjectById;

public sealed class GetVideoProjectByIdHandler
    : IQueryHandler<GetVideoProjectByIdQuery, GetVideoProjectByIdResult>
{
    private readonly IVideoProjectRepository _repository;

    public GetVideoProjectByIdHandler(
        IVideoProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetVideoProjectByIdResult?> HandleAsync(
        GetVideoProjectByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        if (query.VideoProjectId == Guid.Empty)
            return null;

        var project = await _repository.GetByIdAsync(
            query.VideoProjectId,
            cancellationToken);

        if (project is null)
            return null;

        return new GetVideoProjectByIdResult(
            project.Id,
            project.MusicalProjectId,
            project.Name,
            project.Concept,
            project.Duration,
            project.CreatedAt,
            project.Status);
    }
}
