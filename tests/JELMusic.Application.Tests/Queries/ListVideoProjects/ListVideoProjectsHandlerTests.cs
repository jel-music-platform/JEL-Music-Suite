using JELMusic.Application.Queries.ListVideoProjects;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.Entities;

namespace JELMusic.Application.Tests.Queries.ListVideoProjects;

public class ListVideoProjectsHandlerTests
{
    [Fact]
    public async Task Should_return_empty_list_when_no_projects_exist()
    {
        var repository = new FakeVideoProjectRepository();

        var handler = new ListVideoProjectsHandler(repository);

        var result = await handler.HandleAsync(
            new ListVideoProjectsQuery());

        Assert.NotNull(result);
        Assert.Empty(result!.Projects);
    }

    [Fact]
    public async Task Should_return_all_projects()
    {
        var repository = new FakeVideoProjectRepository();

        var musicalProjectId = Guid.NewGuid();

        var firstProject = CreateVideoProject(
            musicalProjectId,
            "Videoclip uno",
            "Concepto uno",
            TimeSpan.FromMinutes(3));

        var secondProject = CreateVideoProject(
            musicalProjectId,
            "Videoclip dos",
            "Concepto dos",
            TimeSpan.FromMinutes(4));

        await repository.AddAsync(firstProject);
        await repository.AddAsync(secondProject);

        var handler = new ListVideoProjectsHandler(repository);

        var result = await handler.HandleAsync(
            new ListVideoProjectsQuery());

        Assert.NotNull(result);
        Assert.Equal(2, result!.Projects.Count);
    }

    [Fact]
    public async Task Should_map_project_properties_correctly()
    {
        var repository = new FakeVideoProjectRepository();

        var musicalProjectId = Guid.NewGuid();

        var project = CreateVideoProject(
            musicalProjectId,
            "Donde Está Tu Corazón - Videoclip",
            "Un hombre descubre que el éxito material no le proporciona satisfacción.",
            TimeSpan.FromMinutes(4) + TimeSpan.FromSeconds(28));

        await repository.AddAsync(project);

        var handler = new ListVideoProjectsHandler(repository);

        var result = await handler.HandleAsync(
            new ListVideoProjectsQuery());

        var item = Assert.Single(result!.Projects);

        Assert.Equal(project.Id, item.VideoProjectId);
        Assert.Equal(project.MusicalProjectId, item.MusicalProjectId);
        Assert.Equal(project.Name, item.Name);
        Assert.Equal(project.Concept, item.Concept);
        Assert.Equal(project.Duration, item.Duration);
        Assert.Equal(project.CreatedAt, item.CreatedAt);
        Assert.Equal(project.Status, item.Status);
    }

    [Fact]
    public async Task Should_propagate_cancellation_token()
    {
        var repository = new FakeVideoProjectRepository();

        var handler = new ListVideoProjectsHandler(repository);

        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        await handler.HandleAsync(
            new ListVideoProjectsQuery(),
            cancellationToken);

        Assert.Equal(
            cancellationToken,
            repository.LastCancellationToken);
    }

    [Fact]
    public async Task Should_throw_when_query_is_null()
    {
        var repository = new FakeVideoProjectRepository();

        var handler = new ListVideoProjectsHandler(repository);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }

    private static VideoProject CreateVideoProject(
        Guid musicalProjectId,
        string name,
        string concept,
        TimeSpan duration)
    {
        return VideoProject.Create(
            musicalProjectId,
            name,
            concept,
            duration);
    }
}
