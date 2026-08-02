using JELMusic.Application.UseCases.CreateMusicalProject;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Application.Tests.UseCases;

public class CreateMusicalProjectTests
{
    [Fact]
    public async Task Should_create_a_musical_project()
    {
        var repository = new FakeMusicalProjectRepository();
        var unitOfWork = new FakeUnitOfWork();

        var useCase = new CreateMusicalProjectUseCase(
            repository,
            unitOfWork);

        var dna = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Worship",
                74,
                "Tenor"));

        var request = new CreateMusicalProjectRequest(
            "Proyecto prueba",
            "Worship",
            "Proyecto de test");

        var result = await useCase.ExecuteAsync(
            request,
            dna);

        Assert.NotEqual(Guid.Empty, result.ProjectId);
        Assert.Single(repository.Projects);
        Assert.True(unitOfWork.Saved);
    }

    [Fact]
    public async Task Should_throw_when_name_is_empty()
    {
        var repository = new FakeMusicalProjectRepository();
        var unitOfWork = new FakeUnitOfWork();

        var useCase = new CreateMusicalProjectUseCase(
            repository,
            unitOfWork);

        var dna = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Worship",
                74,
                "Tenor"));

        var request = new CreateMusicalProjectRequest(
            "",
            "Worship",
            "Proyecto de prueba");

        await Assert.ThrowsAsync<ArgumentException>(() =>
            useCase.ExecuteAsync(request, dna));
    }

    [Fact]
    public async Task Should_throw_when_genre_is_empty()
    {
        var repository = new FakeMusicalProjectRepository();
        var unitOfWork = new FakeUnitOfWork();

        var useCase = new CreateMusicalProjectUseCase(
            repository,
            unitOfWork);

        var dna = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Worship",
                74,
                "Tenor"));

        var request = new CreateMusicalProjectRequest(
            "Proyecto prueba",
            "",
            "Proyecto de prueba");

        await Assert.ThrowsAsync<ArgumentException>(() =>
            useCase.ExecuteAsync(request, dna));
    }
}