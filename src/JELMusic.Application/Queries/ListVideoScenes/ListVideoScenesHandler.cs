using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Queries.ListVideoScenes;

public sealed class ListVideoScenesHandler
    : IQueryHandler<ListVideoScenesQuery, ListVideoScenesResult>
{
    private readonly IVideoSceneRepository _repository;

    public ListVideoScenesHandler(
        IVideoSceneRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListVideoScenesResult?> HandleAsync(
        ListVideoScenesQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var scenes = await _repository.GetAllAsync(
            cancellationToken);

        var items = scenes
            .Where(scene => scene.VideoProjectId == query.VideoProjectId)
            .OrderBy(scene => scene.StartTime)
            .Select(scene => new VideoSceneListItem(
                scene.Id,
                scene.VideoProjectId,
                scene.Name,
                scene.StartTime,
                scene.EndTime,
                scene.Duration,
                scene.Description))
            .ToList();

        return new ListVideoScenesResult(items);
    }
}
