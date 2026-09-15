using JELMusic.Infrastructure.Persistence;
using JELMusic.Infrastructure.Tests.Builders;
using JELMusic.Infrastructure.Tests.Infrastructure;
using Xunit;

namespace JELMusic.Infrastructure.Tests.Persistence;

public sealed class CoreUnitOfWorkTests
{
    [Fact]
    public async Task SaveChangesAsync_should_persist_pending_changes()
    {
        using var database = new SqliteTestDatabase();
        var unitOfWork = new CoreUnitOfWork(database.Context);
        var project = MusicalProjectBuilder.Create();

        database.Context.MusicalProjects.Add(project);

        var affectedRows = await unitOfWork.SaveChangesAsync();

        database.Context.ChangeTracker.Clear();

        var loaded = await database.Context.MusicalProjects.FindAsync(project.Id);

        Assert.Equal(3, affectedRows);
        Assert.NotNull(loaded);
        Assert.Equal(project.Id, loaded!.Id);
        Assert.Equal(project.Name, loaded.Name);
    }
}