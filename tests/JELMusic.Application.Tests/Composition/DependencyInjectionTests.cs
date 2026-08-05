using JELMusic.Application;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.Repositories;
using JELMusic.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Application.Tests.Composition;

public class DependencyInjectionTests
{
    [Fact]
    public void Should_register_create_project_handler()
    {
        var services = new ServiceCollection();

        services.AddScoped<IMusicalProjectRepository, FakeMusicalProjectRepository>();
        services.AddScoped<IUnitOfWork, FakeUnitOfWork>();
        services.AddScoped<IMusicalProjectFactory, FakeMusicalProjectFactory>();

        services.AddApplication();

        using var provider = services.BuildServiceProvider();

        var handler = provider.GetService<
            ICommandHandler<CreateProjectCommand, Guid>>();

        Assert.NotNull(handler);
        Assert.IsType<CreateProjectCommandHandler>(handler);
    }
}