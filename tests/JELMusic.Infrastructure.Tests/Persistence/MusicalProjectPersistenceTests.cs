using System.Linq;
using JELMusic.Infrastructure.Tests.Builders;
using JELMusic.Infrastructure.Tests.Infrastructure;
using Xunit;

namespace JELMusic.Infrastructure.Tests.Persistence;

public sealed class MusicalProjectPersistenceTests
{
    [Fact]
    public void Should_persist_and_reload_complete_musical_project()
    {
        using var database = new SqliteTestDatabase();

        var project = MusicalProjectBuilder.Create();

        database.Context.MusicalProjects.Add(project);
        database.Context.SaveChanges();

        database.Context.ChangeTracker.Clear();

        var loaded = database.Context.MusicalProjects.Single();

        Assert.NotNull(loaded);

        Assert.Equal(project.Name, loaded.Name);
        Assert.Equal(project.Genre, loaded.Genre);
        Assert.Equal(project.Description, loaded.Description);

        Assert.NotNull(loaded.DNA);

        Assert.NotNull(loaded.DNA.Style);
        Assert.Equal("Folk Vasco", loaded.DNA.Style!.Name);

        Assert.NotNull(loaded.DNA.Performance);
        Assert.Equal(90, loaded.DNA.Performance.TempoBpm);

        Assert.Single(loaded.DNA.InfluenceProfiles);
        Assert.Single(loaded.DNA.InstrumentProfiles);

        Assert.Equal(
            "Folk Vasco",
            loaded.DNA.InfluenceProfiles.First().Name);

        Assert.Equal(
            "Trikitixa",
            loaded.DNA.InstrumentProfiles.First().Name);
    }
}