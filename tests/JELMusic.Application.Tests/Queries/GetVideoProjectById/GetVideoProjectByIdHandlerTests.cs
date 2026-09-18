using JELMusic.Application.Queries.GetVideoProjectById;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.Entities;
using Xunit;

namespace JELMusic.Application.Tests.Queries.GetVideoProjectById;

public class GetVideoProjectByIdHandlerTests
{
    [Fact]
    public async Task HandleAsync_WhenProjectExists_ReturnsProject()
    {
        var repository = new FakeVideoProjectRepository();

        var musicalProjectId = Guid.NewGuid();

        var project = VideoProject.Create(
            musicalProjectId,
            "Donde Está Tu Corazón",
            "Un hombre descubre que el éxito material no le proporciona satisfacción.",
            TimeSpan.FromMinutes(4) + TimeSpan.FromSeconds(28));

        repository.Projects.Add(project);

        var handler = new GetVideoProjectByIdHandler(repository);

        var result = await handler.HandleAsync(
            new GetVideoProjectByIdQuery(project.Id));

        Assert.NotNull(result);
        Assert.Equal(project.Id, result.VideoProjectId);
        Assert.Equal(musicalProjectId, result.MusicalProjectId);
        Assert.Equal("Donde Está Tu Corazón", result.Name);
        Assert.Equal(
            "Un hombre descubre que el éxito material no le proporciona satisfacción.",
            result.Concept);
        Assert.Equal(
            TimeSpan.FromMinutes(4) + TimeSpan.FromSeconds(28),
            result.Duration);
        Assert.Equal(project.CreatedAt, result.CreatedAt);
        Assert.Equal(project.Status, result.Status);
    }

    [Fact]
    public async Task HandleAsync_WhenProjectDoesNotExist_ReturnsNull()
    {
        var repository = new FakeVideoProjectRepository();

        var handler = new GetVideoProjectByIdHandler(repository);

        var result = await handler.HandleAsync(
            new GetVideoProjectByIdQuery(Guid.NewGuid()));

        Assert.Null(result);
    }

    [Fact]
    public async Task HandleAsync_WhenIdIsEmpty_ReturnsNull()
    {
        var repository = new FakeVideoProjectRepository();

        var handler = new GetVideoProjectByIdHandler(repository);

        var result = await handler.HandleAsync(
            new GetVideoProjectByIdQuery(Guid.Empty));

        Assert.Null(result);
    }

    [Fact]
    public async Task HandleAsync_WhenQueryIsNull_ThrowsArgumentNullException()
    {
        var repository = new FakeVideoProjectRepository();

        var handler = new GetVideoProjectByIdHandler(repository);

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!));
    }

    [Fact]
    public async Task HandleAsync_PassesCancellationTokenToRepository()
    {
        var repository = new FakeVideoProjectRepository();

        var project = VideoProject.Create(
            Guid.NewGuid(),
            "Proyecto de prueba",
            "Concepto de prueba",
            TimeSpan.FromMinutes(2));

        repository.Projects.Add(project);

        using var cancellationTokenSource =
            new CancellationTokenSource();

        var cancellationToken = cancellationTokenSource.Token;

        var handler = new GetVideoProjectByIdHandler(repository);

        await handler.HandleAsync(
            new GetVideoProjectByIdQuery(project.Id),
            cancellationToken);

        Assert.Equal(
            cancellationToken,
            repository.LastCancellationToken);
    }
}
