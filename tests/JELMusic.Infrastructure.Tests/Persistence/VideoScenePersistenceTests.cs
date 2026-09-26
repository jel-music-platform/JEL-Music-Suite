using JELMusic.Domain.Entities;
using JELMusic.Infrastructure.Tests.Builders;
using JELMusic.Infrastructure.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace JELMusic.Infrastructure.Tests;

public class VideoScenePersistenceTests
{
    [Fact]
    public async Task CanPersistAndReloadVideoScene()
    {
        using var database = new SqliteTestDatabase();

        var musicalProject = MusicalProjectBuilder.Create();

        var videoProject = VideoProject.Create(
            musicalProject.Id,
            "Jig de Dublín",
            "Videoclip ambientado en una taberna irlandesa.",
            TimeSpan.FromMinutes(2) + TimeSpan.FromSeconds(40));

        var scene = VideoScene.Create(
            videoProject.Id,
            "Entrada a la taberna",
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(35),
            "Johnny entra en la taberna.");

        database.Context.Add(musicalProject);
        database.Context.Add(videoProject);
        database.Context.Add(scene);

        await database.Context.SaveChangesAsync();

        database.Context.ChangeTracker.Clear();

        var reloadedScene = await database.Context.VideoScenes
            .SingleAsync();

        Assert.Equal(videoProject.Id, reloadedScene.VideoProjectId);
        Assert.Equal("Entrada a la taberna", reloadedScene.Name);
        Assert.Equal(
            TimeSpan.FromSeconds(10),
            reloadedScene.StartTime);
        Assert.Equal(
            TimeSpan.FromSeconds(35),
            reloadedScene.EndTime);
        Assert.Equal(
            "Johnny entra en la taberna.",
            reloadedScene.Description);
        Assert.Equal(
            TimeSpan.FromSeconds(25),
            reloadedScene.Duration);
    }
}