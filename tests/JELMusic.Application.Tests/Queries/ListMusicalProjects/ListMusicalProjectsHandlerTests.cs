using JELMusic.Application.Queries.ListMusicalProjects;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.Entities;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Application.Tests.Queries.ListMusicalProjects;

public class ListMusicalProjectsHandlerTests
{
    [Fact]
    public async Task Should_return_empty_list_when_no_projects_exist()
    {
        var repository = new FakeMusicalProjectRepository();

        var handler = new ListMusicalProjectsHandler(repository);

        var result = await handler.HandleAsync(
            new ListMusicalProjectsQuery());

        Assert.NotNull(result);
        Assert.Empty(result!.Projects);
    }

    [Fact]
    public async Task Should_return_all_projects()
    {
        var repository = new FakeMusicalProjectRepository();

        var firstProject = CreateProject(
            "Proyecto uno",
            "Pop",
            "Descripción uno");

        var secondProject = CreateProject(
            "Proyecto dos",
            "Folk",
            "Descripción dos");

        await repository.AddAsync(firstProject);
        await repository.AddAsync(secondProject);

        var handler = new ListMusicalProjectsHandler(repository);

        var result = await handler.HandleAsync(
            new ListMusicalProjectsQuery());

        Assert.NotNull(result);
        Assert.Equal(2, result!.Projects.Count);
    }

    [Fact]
    public async Task Should_map_project_properties_correctly()
    {
        var repository = new FakeMusicalProjectRepository();

        var project = CreateProject(
            "Proyecto prueba",
            "Worship",
            "Descripción de prueba");

        await repository.AddAsync(project);

        var handler = new ListMusicalProjectsHandler(repository);

        var result = await handler.HandleAsync(
            new ListMusicalProjectsQuery());

        var item = Assert.Single(result!.Projects);

        Assert.Equal(project.Id, item.ProjectId);
        Assert.Equal(project.Name, item.Name);
        Assert.Equal(project.Genre, item.Genre);
        Assert.Equal(project.Description, item.Description);
    }

    [Fact]
    public async Task Should_propagate_cancellation_token()
    {
        var repository = new FakeMusicalProjectRepository();

        var handler = new ListMusicalProjectsHandler(repository);

        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        await handler.HandleAsync(
            new ListMusicalProjectsQuery(),
            cancellationToken);

        Assert.Equal(
            cancellationToken,
            repository.LastCancellationToken);
    }

    [Fact]
    public async Task Should_throw_when_query_is_null()
    {
        var repository = new FakeMusicalProjectRepository();

        var handler = new ListMusicalProjectsHandler(repository);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }

    private static MusicalProject CreateProject(
        string name,
        string genre,
        string description)
    {
        var dna = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Neutral",
                120,
                "Instrumental"));

        return MusicalProject.Create(
            name,
            genre,
            description,
            dna);
    }
}
