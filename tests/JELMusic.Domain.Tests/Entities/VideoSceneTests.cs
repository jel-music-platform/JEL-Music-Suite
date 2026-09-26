using JELMusic.Domain.Entities;

namespace JELMusic.Domain.Tests.Entities;

public class VideoSceneTests
{
    [Fact]
    public void Create_should_create_valid_video_scene()
    {
        var videoProjectId = Guid.NewGuid();
        var startTime = TimeSpan.FromSeconds(10);
        var endTime = TimeSpan.FromSeconds(45);

        var scene = VideoScene.Create(
            videoProjectId,
            "Entrada a la taberna",
            startTime,
            endTime,
            "Johnny entra en la taberna y observa el ambiente.");

        Assert.NotEqual(Guid.Empty, scene.Id);
        Assert.Equal(videoProjectId, scene.VideoProjectId);
        Assert.Equal("Entrada a la taberna", scene.Name);
        Assert.Equal(startTime, scene.StartTime);
        Assert.Equal(endTime, scene.EndTime);
        Assert.Equal(
            "Johnny entra en la taberna y observa el ambiente.",
            scene.Description);
    }

    [Fact]
    public void Create_should_calculate_duration_from_start_and_end()
    {
        var scene = VideoScene.Create(
            Guid.NewGuid(),
            "Escena principal",
            TimeSpan.FromSeconds(20),
            TimeSpan.FromSeconds(65),
            "Descripción");

        Assert.Equal(TimeSpan.FromSeconds(45), scene.Duration);
    }

    [Fact]
    public void Create_should_allow_zero_start_time()
    {
        var scene = VideoScene.Create(
            Guid.NewGuid(),
            "Inicio",
            TimeSpan.Zero,
            TimeSpan.FromSeconds(30),
            "Descripción");

        Assert.Equal(TimeSpan.Zero, scene.StartTime);
    }

    [Fact]
    public void Create_should_throw_when_video_project_id_is_empty()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => VideoScene.Create(
                Guid.Empty,
                "Escena",
                TimeSpan.Zero,
                TimeSpan.FromSeconds(30),
                "Descripción"));

        Assert.Equal("videoProjectId", exception.ParamName);
    }

    [Fact]
    public void Create_should_throw_when_name_is_empty()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => VideoScene.Create(
                Guid.NewGuid(),
                "",
                TimeSpan.Zero,
                TimeSpan.FromSeconds(30),
                "Descripción"));

        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Create_should_throw_when_start_time_is_negative()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => VideoScene.Create(
                Guid.NewGuid(),
                "Escena",
                TimeSpan.FromSeconds(-1),
                TimeSpan.FromSeconds(30),
                "Descripción"));

        Assert.Equal("startTime", exception.ParamName);
    }

    [Fact]
    public void Create_should_throw_when_end_time_is_equal_to_start_time()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => VideoScene.Create(
                Guid.NewGuid(),
                "Escena",
                TimeSpan.FromSeconds(30),
                TimeSpan.FromSeconds(30),
                "Descripción"));

        Assert.Equal("endTime", exception.ParamName);
    }

    [Fact]
    public void Create_should_throw_when_end_time_is_before_start_time()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => VideoScene.Create(
                Guid.NewGuid(),
                "Escena",
                TimeSpan.FromSeconds(40),
                TimeSpan.FromSeconds(30),
                "Descripción"));

        Assert.Equal("endTime", exception.ParamName);
    }
}
