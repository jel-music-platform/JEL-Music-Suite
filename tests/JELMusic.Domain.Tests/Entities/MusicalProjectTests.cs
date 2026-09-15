using JELMusic.Domain.Entities;
using JELMusic.Domain.Enums;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Domain.Tests.Entities;

public class MusicalProjectTests
{
    private static MusicalDNA CreateDNA()
    {
        return new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Neutral",
                120,
                "Instrumental"));
    }

    [Fact]
    public void Create_should_create_valid_project()
    {
        var dna = CreateDNA();

        var project = MusicalProject.Create(
            "Proyecto prueba",
            "Pop",
            "Descripción de prueba",
            dna);

        Assert.NotEqual(Guid.Empty, project.Id);
        Assert.Equal("Proyecto prueba", project.Name);
        Assert.Equal("Pop", project.Genre);
        Assert.Equal("Descripción de prueba", project.Description);
        Assert.Same(dna, project.DNA);
        Assert.Equal(ProjectStatus.Draft, project.Status);
        Assert.Equal(1, project.ProjectVersion);
        Assert.NotEqual(default, project.CreatedAt);
    }

    [Fact]
    public void Create_should_throw_when_name_is_empty()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => MusicalProject.Create(
                "",
                "Pop",
                "Descripción",
                CreateDNA()));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Create_should_throw_when_genre_is_empty()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => MusicalProject.Create(
                "Proyecto",
                "",
                "Descripción",
                CreateDNA()));

        Assert.Equal("genre", exception.ParamName);
    }

    [Fact]
    public void Create_should_throw_when_dna_is_null()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => MusicalProject.Create(
                "Proyecto",
                "Pop",
                "Descripción",
                null!));

        Assert.Equal("dna", exception.ParamName);
    }

    [Fact]
    public void Update_should_change_project_data_and_increment_version()
    {
        var originalDNA = CreateDNA();
        var updatedDNA = CreateDNA();

        var project = MusicalProject.Create(
            "Proyecto original",
            "Pop",
            "Descripción original",
            originalDNA);

        var originalVersion = project.ProjectVersion;

        project.Update(
            "Proyecto actualizado",
            "Folk",
            "Descripción actualizada",
            updatedDNA);

        Assert.Equal("Proyecto actualizado", project.Name);
        Assert.Equal("Folk", project.Genre);
        Assert.Equal("Descripción actualizada", project.Description);
        Assert.Same(updatedDNA, project.DNA);
        Assert.Equal(originalVersion + 1, project.ProjectVersion);
    }

    [Fact]
    public void Update_should_throw_when_name_is_empty()
    {
        var project = MusicalProject.Create(
            "Proyecto",
            "Pop",
            "Descripción",
            CreateDNA());

        var exception = Assert.Throws<ArgumentException>(
            () => project.Update(
                "",
                "Folk",
                "Descripción actualizada",
                CreateDNA()));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Update_should_throw_when_genre_is_empty()
    {
        var project = MusicalProject.Create(
            "Proyecto",
            "Pop",
            "Descripción",
            CreateDNA());

        var exception = Assert.Throws<ArgumentException>(
            () => project.Update(
                "Proyecto actualizado",
                "",
                "Descripción actualizada",
                CreateDNA()));

        Assert.Equal("genre", exception.ParamName);
    }

    [Fact]
    public void Update_should_throw_when_dna_is_null()
    {
        var project = MusicalProject.Create(
            "Proyecto",
            "Pop",
            "Descripción",
            CreateDNA());

        var exception = Assert.Throws<ArgumentNullException>(
            () => project.Update(
                "Proyecto actualizado",
                "Folk",
                "Descripción actualizada",
                null!));

        Assert.Equal("dna", exception.ParamName);
    }
}