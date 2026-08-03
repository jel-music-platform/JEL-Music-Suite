using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.Entities;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Application.Tests.Queries.GetMusicalProjectById;

public class GetMusicalProjectByIdHandlerTests
{
    [Fact]
    public async Task Should_return_project_when_it_exists()
    {
        var repository = new FakeMusicalProjectRepository();

        var dna = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Worship",
                74,
                "Tenor"));

        var project = MusicalProject.Create(
            "Proyecto prueba",
            "Worship",
            "Descripción",
            dna);

        await repository.AddAsync(project);

        var handler = new GetMusicalProjectByIdHandler(repository);

        var result = await handler.ExecuteAsync(
            new GetMusicalProjectByIdQuery(project.Id));

        Assert.NotNull(result);
        Assert.Equal(project.Id, result!.ProjectId);
        Assert.Equal("Proyecto prueba", result.Name);
        Assert.Equal("Worship", result.Genre);
        Assert.Equal("Descripción", result.Description);
    }
   [Fact]
public async Task Should_return_null_when_project_does_not_exist()
{
    var repository = new FakeMusicalProjectRepository();

    var handler = new GetMusicalProjectByIdHandler(repository);

    var result = await handler.ExecuteAsync(
        new GetMusicalProjectByIdQuery(Guid.NewGuid()));

    Assert.Null(result);
}
}
