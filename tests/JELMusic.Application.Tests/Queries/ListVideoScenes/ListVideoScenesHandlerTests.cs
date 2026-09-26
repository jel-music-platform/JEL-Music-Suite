using JELMusic.Application.Queries.ListVideoScenes;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.Entities;

namespace JELMusic.Application.Tests.Queries.ListVideoScenes;

public class ListVideoScenesHandlerTests
{
    [Fact]
    public async Task Should_return_empty_list_when_no_scenes_exist()
    {
        var repository = new FakeVideoSceneRepository();

        var handler = new ListVideoScenesHandler(repository);

        var result = await handler.HandleAsync(
            new ListVideoScenesQuery(Guid.NewGuid()));

        Assert.NotNull(result);
        Assert.Empty(result!.Scenes);
    }

    [Fact]
    public async Task Should_return_only_scenes_for_requested_video_project()
    {
        var repository = new FakeVideoSceneRepository();

        var firstProjectId = Guid.NewGuid();
        var secondProjectId = Guid.NewGuid();

        var firstScene = CreateVideoScene(
            firstProjectId,
            "Escena uno",
            TimeSpan.FromSeconds(0),
            TimeSpan.FromSeconds(30),
            "Primera escena");

        var secondScene = CreateVideoScene(
            secondProjectId,
            "Escena dos",
            TimeSpan.FromSeconds(30),
            TimeSpan.FromSeconds(60),
            "Segunda escena");

        await repository.AddAsync(firstScene);
        await repository.AddAsync(secondScene);

        var handler = new ListVideoScenesHandler(repository);

        var result = await handler.HandleAsync(
            new ListVideoScenesQuery(firstProjectId));

        var item = Assert.Single(result!.Scenes);

        Assert.Equal(firstScene.Id, item.VideoSceneId);
        Assert.Equal(firstProjectId, item.VideoProjectId);
    }

    [Fact]
    public async Task Should_order_scenes_by_start_time()
    {
        var repository = new FakeVideoSceneRepository();

        var videoProjectId = Guid.NewGuid();

        var secondScene = CreateVideoScene(
            videoProjectId,
            "Escena dos",
            TimeSpan.FromSeconds(60),
            TimeSpan.FromSeconds(90),
            "Segunda escena");

        var firstScene = CreateVideoScene(
            videoProjectId,
            "Escena uno",
            TimeSpan.FromSeconds(0),
            TimeSpan.FromSeconds(30),
            "Primera escena");

        await repository.AddAsync(secondScene);
        await repository.AddAsync(firstScene);

        var handler = new ListVideoScenesHandler(repository);

        var result = await handler.HandleAsync(
            new ListVideoScenesQuery(videoProjectId));

        Assert.NotNull(result);
        Assert.Equal(2, result!.Scenes.Count);

        Assert.Equal(
            firstScene.Id,
            result.Scenes[0].VideoSceneId);

        Assert.Equal(
            secondScene.Id,
            result.Scenes[1].VideoSceneId);
    }

    [Fact]
    public async Task Should_map_scene_properties_correctly()
    {
        var repository = new FakeVideoSceneRepository();

        var videoProjectId = Guid.NewGuid();

        var scene = CreateVideoScene(
            videoProjectId,
            "Entrada en la taberna",
            TimeSpan.FromSeconds(12),
            TimeSpan.FromSeconds(47),
            "Johnny entra en la taberna.");

        await repository.AddAsync(scene);

        var handler = new ListVideoScenesHandler(repository);

        var result = await handler.HandleAsync(
            new ListVideoScenesQuery(videoProjectId));

        var item = Assert.Single(result!.Scenes);

        Assert.Equal(scene.Id, item.VideoSceneId);
        Assert.Equal(scene.VideoProjectId, item.VideoProjectId);
        Assert.Equal(scene.Name, item.Name);
        Assert.Equal(scene.StartTime, item.StartTime);
        Assert.Equal(scene.EndTime, item.EndTime);
        Assert.Equal(scene.Duration, item.Duration);
        Assert.Equal(scene.Description, item.Description);
    }

    [Fact]
    public async Task Should_propagate_cancellation_token()
    {
        var repository = new FakeVideoSceneRepository();

        var handler = new ListVideoScenesHandler(repository);

        using var cancellationTokenSource =
            new CancellationTokenSource();

        var cancellationToken =
            cancellationTokenSource.Token;

        await handler.HandleAsync(
            new ListVideoScenesQuery(Guid.NewGuid()),
            cancellationToken);

        Assert.Equal(
            cancellationToken,
            repository.LastCancellationToken);
    }

    [Fact]
    public async Task Should_throw_when_query_is_null()
    {
        var repository = new FakeVideoSceneRepository();

        var handler = new ListVideoScenesHandler(repository);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }

    private static VideoScene CreateVideoScene(
        Guid videoProjectId,
        string name,
        TimeSpan startTime,
        TimeSpan endTime,
        string description)
    {
        return VideoScene.Create(
            videoProjectId,
            name,
            startTime,
            endTime,
            description);
    }
}