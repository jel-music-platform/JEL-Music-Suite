using JELMusic.Domain.Entities;
using JELMusic.Domain.Enums;

namespace JELMusic.Domain.Tests.Entities;

public class VideoProjectTests
{
    [Fact]
    public void Create_should_create_valid_video_project()
    {
        var musicalProjectId = Guid.NewGuid();
        var duration = TimeSpan.FromMinutes(4) + TimeSpan.FromSeconds(28);

        var project = VideoProject.Create(
            musicalProjectId,
            "Donde Está Tu Corazón",
            "Un hombre descubre que el éxito material no le proporciona satisfacción.",
            duration);

        Assert.NotEqual(Guid.Empty, project.Id);
        Assert.Equal(musicalProjectId, project.MusicalProjectId);
        Assert.Equal("Donde Está Tu Corazón", project.Name);
        Assert.Equal(
            "Un hombre descubre que el éxito material no le proporciona satisfacción.",
            project.Concept);
        Assert.Equal(duration, project.Duration);
        Assert.Equal(VideoProjectStatus.Draft, project.Status);
        Assert.NotEqual(default, project.CreatedAt);
    }

    [Fact]
    public void Create_should_allow_zero_duration()
    {
        var project = VideoProject.Create(
            Guid.NewGuid(),
            "Proyecto audiovisual",
            "Concepto",
            TimeSpan.Zero);

        Assert.Equal(TimeSpan.Zero, project.Duration);
    }

    [Fact]
    public void Create_should_throw_when_musical_project_id_is_empty()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => VideoProject.Create(
                Guid.Empty,
                "Proyecto audiovisual",
                "Concepto",
                TimeSpan.FromMinutes(3)));

        Assert.Equal("musicalProjectId", exception.ParamName);
    }

    [Fact]
    public void Create_should_throw_when_name_is_empty()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => VideoProject.Create(
                Guid.NewGuid(),
                "",
                "Concepto",
                TimeSpan.FromMinutes(3)));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Create_should_throw_when_duration_is_negative()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => VideoProject.Create(
                Guid.NewGuid(),
                "Proyecto audiovisual",
                "Concepto",
                TimeSpan.FromSeconds(-1)));

        Assert.Equal("duration", exception.ParamName);
    }
}
