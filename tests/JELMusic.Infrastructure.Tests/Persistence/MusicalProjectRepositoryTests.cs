using JELMusic.Infrastructure.Persistence.Repositories;
using JELMusic.Infrastructure.Tests.Builders;
using JELMusic.Infrastructure.Tests.Infrastructure;
using Xunit;

namespace JELMusic.Infrastructure.Tests.Persistence;

public sealed class MusicalProjectRepositoryTests
{
    [Fact]
    public async Task AddAsync_should_persist_project_and_GetByIdAsync_should_reload_it()
    {
        using var database = new SqliteTestDatabase();
        var repository = new MusicalProjectRepository(database.Context);
        var project = MusicalProjectBuilder.Create();

        await repository.AddAsync(project);
        await database.Context.SaveChangesAsync();
        database.Context.ChangeTracker.Clear();

        var loaded = await repository.GetByIdAsync(project.Id);

        Assert.NotNull(loaded);
        Assert.Equal(project.Id, loaded!.Id);
        Assert.Equal(project.Name, loaded.Name);
        Assert.Equal(project.Genre, loaded.Genre);
    }

    [Fact]
    public async Task GetByIdAsync_should_return_null_when_project_does_not_exist()
    {
        using var database = new SqliteTestDatabase();
        var repository = new MusicalProjectRepository(database.Context);

        var loaded = await repository.GetByIdAsync(Guid.NewGuid());

        Assert.Null(loaded);
    }

    [Fact]
    public async Task GetAllAsync_should_return_all_projects()
    {
        using var database = new SqliteTestDatabase();
        var repository = new MusicalProjectRepository(database.Context);
        var project1 = MusicalProjectBuilder.Create();
        var project2 = MusicalProjectBuilder.Create();

        await repository.AddAsync(project1);
        await repository.AddAsync(project2);
        await database.Context.SaveChangesAsync();

        var projects = await repository.GetAllAsync();

        Assert.Equal(2, projects.Count);
        Assert.Contains(project1, projects);
        Assert.Contains(project2, projects);
    }

    [Fact]
    public async Task Update_should_persist_project_changes()
    {
        using var database = new SqliteTestDatabase();
        var repository = new MusicalProjectRepository(database.Context);
        var project = MusicalProjectBuilder.Create();

        await repository.AddAsync(project);
        await database.Context.SaveChangesAsync();

        project.Update(
            "Proyecto Actualizado",
            "Pop",
            "Descripción actualizada",
            project.DNA);

        repository.Update(project);
        await database.Context.SaveChangesAsync();
        database.Context.ChangeTracker.Clear();

        var loaded = await repository.GetByIdAsync(project.Id);

        Assert.NotNull(loaded);
        Assert.Equal("Proyecto Actualizado", loaded!.Name);
        Assert.Equal("Pop", loaded.Genre);
        Assert.Equal("Descripción actualizada", loaded.Description);
        Assert.Equal(2, loaded.ProjectVersion);
    }

    [Fact]
    public async Task Remove_should_delete_project()
    {
        using var database = new SqliteTestDatabase();
        var repository = new MusicalProjectRepository(database.Context);
        var project = MusicalProjectBuilder.Create();

        await repository.AddAsync(project);
        await database.Context.SaveChangesAsync();

        repository.Remove(project);
        await database.Context.SaveChangesAsync();
        database.Context.ChangeTracker.Clear();

        var loaded = await repository.GetByIdAsync(project.Id);

        Assert.Null(loaded);
    }
}
