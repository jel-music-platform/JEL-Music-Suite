using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.Entities;
using JELMusic.Domain.Enums;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Application.Tests.Queries.GetMusicalProjectById;

public class GetMusicalProjectByIdHandlerTests
{
    [Fact]
    public async Task Should_return_project_with_complete_musical_dna_when_it_exists()
    {
        var repository = new FakeMusicalProjectRepository();

        var influenceOrigin = new KnowledgeOrigin(
            "Archivo musical",
            "REF-001",
            "Tradici\u00F3n vasca");

        var instrumentOrigin = new KnowledgeOrigin(
            "Investigaci\u00F3n propia",
            "REF-002",
            "M\u00FAsica vasca");

        var styleOrigin = new KnowledgeOrigin(
            "Biblioteca musical",
            "REF-003",
            "Cultura vasca");

        var influence = new InfluenceProfile(
            "Folk Vasco",
            "Cultural",
            "Influencia tradicional vasca",
            "Tradici\u00F3n vasca",
            "Aporta identidad mel\u00F3dica y r\u00EDtmica",
            influenceOrigin);

        var instrument = new InstrumentProfile(
            "Trikitixa",
            "Acorde\u00F3n",
            "Base arm\u00F3nica y r\u00EDtmica",
            "Instrumento tradicional vasco",
            "M\u00FAsica vasca",
            "C\u00E1lido y mel\u00F3dico",
            instrumentOrigin);

        var style = new MusicalStyleProfile(
            "Folk Vasco",
            MusicalGenre.Folk,
            "Tradicional, org\u00E1nico y mel\u00F3dico",
            "Estilo folk de ra\u00EDz vasca",
            "Tradici\u00F3n musical vasca",
            "Org\u00E1nico y cercano",
            styleOrigin);

        var dna = new MusicalDNA(
            new[] { influence },
            new[] { instrument },
            new PerformanceProfile(
                "Worship",
                74,
                "Tenor"),
            style);

        var project = MusicalProject.Create(
            "Proyecto prueba",
            "Worship",
            "Descripci\u00F3n",
            dna);

        await repository.AddAsync(project);

        var handler = new GetMusicalProjectByIdHandler(repository);

        var result = await handler.HandleAsync(
            new GetMusicalProjectByIdQuery(project.Id));

        Assert.NotNull(result);
        Assert.Equal(project.Id, result!.ProjectId);
        Assert.Equal("Proyecto prueba", result.Name);
        Assert.Equal("Worship", result.Genre);
        Assert.Equal("Descripci\u00F3n", result.Description);

        Assert.NotNull(result.MusicalDNA);

        Assert.Single(result.MusicalDNA.InfluenceProfiles);
        var influenceResult = result.MusicalDNA.InfluenceProfiles[0];

        Assert.Equal("Folk Vasco", influenceResult.Name);
        Assert.Equal(
            "Influencia tradicional vasca",
            influenceResult.Description);
        Assert.Equal(
            "Tradici\u00F3n vasca",
            influenceResult.CulturalContext);
        Assert.Equal("Cultural", influenceResult.InfluenceType);
        Assert.Equal(
            "Aporta identidad mel\u00F3dica y r\u00EDtmica",
            influenceResult.MusicalContribution);

        Assert.NotNull(influenceResult.Origin);
        Assert.Equal(
            "Archivo musical",
            influenceResult.Origin!.Source);
        Assert.Equal(
            "REF-001",
            influenceResult.Origin.Reference);
        Assert.Equal(
            "Tradici\u00F3n vasca",
            influenceResult.Origin.CulturalContext);
        Assert.Equal(
            influenceOrigin.RegisteredAt,
            influenceResult.Origin.RegisteredAt);

        Assert.Single(result.MusicalDNA.InstrumentProfiles);
        var instrumentResult = result.MusicalDNA.InstrumentProfiles[0];

        Assert.Equal("Trikitixa", instrumentResult.Name);
        Assert.Equal(
            "Acorde\u00F3n",
            instrumentResult.Family);
        Assert.Equal(
            "Base arm\u00F3nica y r\u00EDtmica",
            instrumentResult.Function);
        Assert.Equal(
            "Instrumento tradicional vasco",
            instrumentResult.Description);
        Assert.Equal(
            "M\u00FAsica vasca",
            instrumentResult.CulturalContext);
        Assert.Equal(
            "C\u00E1lido y mel\u00F3dico",
            instrumentResult.Character);

        Assert.NotNull(instrumentResult.Origin);
        Assert.Equal(
            "Investigaci\u00F3n propia",
            instrumentResult.Origin!.Source);
        Assert.Equal(
            "REF-002",
            instrumentResult.Origin.Reference);
        Assert.Equal(
            "M\u00FAsica vasca",
            instrumentResult.Origin.CulturalContext);
        Assert.Equal(
            instrumentOrigin.RegisteredAt,
            instrumentResult.Origin.RegisteredAt);

        Assert.NotNull(result.MusicalDNA.Style);
        var styleResult = result.MusicalDNA.Style!;

        Assert.Equal("Folk Vasco", styleResult.Name);
        Assert.Equal(
            MusicalGenre.Folk,
            styleResult.Genre);
        Assert.Equal(
            "Tradicional, org\u00E1nico y mel\u00F3dico",
            styleResult.Characteristics);
        Assert.Equal(
            "Estilo folk de ra\u00EDz vasca",
            styleResult.Description);
        Assert.Equal(
            "Tradici\u00F3n musical vasca",
            styleResult.CulturalContext);
        Assert.Equal(
            "Org\u00E1nico y cercano",
            styleResult.Character);

        Assert.NotNull(styleResult.Origin);
        Assert.Equal(
            "Biblioteca musical",
            styleResult.Origin!.Source);
        Assert.Equal(
            "REF-003",
            styleResult.Origin.Reference);
        Assert.Equal(
            "Cultura vasca",
            styleResult.Origin.CulturalContext);
        Assert.Equal(
            styleOrigin.RegisteredAt,
            styleResult.Origin.RegisteredAt);

        Assert.Equal(
            "Worship",
            result.MusicalDNA.Performance.Mood);
        Assert.Equal(
            74,
            result.MusicalDNA.Performance.TempoBpm);
        Assert.Equal(
            "Tenor",
            result.MusicalDNA.Performance.VocalStyle);
    }

    [Fact]
    public async Task Should_return_null_when_project_does_not_exist()
    {
        var repository = new FakeMusicalProjectRepository();

        var handler = new GetMusicalProjectByIdHandler(repository);

        var result = await handler.HandleAsync(
            new GetMusicalProjectByIdQuery(Guid.NewGuid()));

        Assert.Null(result);
    }

    [Fact]
    public async Task Should_throw_when_query_is_null()
    {
        var repository = new FakeMusicalProjectRepository();

        var handler = new GetMusicalProjectByIdHandler(repository);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }
}