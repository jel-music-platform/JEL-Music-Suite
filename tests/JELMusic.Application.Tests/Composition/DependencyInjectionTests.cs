using JELMusic.Application;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Projects.UpdateProject;
using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.Repositories;
using JELMusic.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Application.Tests.Composition;

public class DependencyInjectionTests
{
    [Fact]
    public void Should_register_application_services()
    {
        var services = new ServiceCollection();

        services.AddScoped<IMusicalProjectRepository, FakeMusicalProjectRepository>();
        services.AddScoped<IUnitOfWork, FakeUnitOfWork>();

        services.AddApplication();

        using var provider = services.BuildServiceProvider();

        var createHandler = provider.GetRequiredService<
            ICommandHandler<CreateProjectCommand, Guid>>();

        var updateHandler = provider.GetRequiredService<
            ICommandHandler<UpdateProjectCommand, UpdateProjectResult>>();

        var queryHandler = provider.GetRequiredService<
            IQueryHandler<
                GetMusicalProjectByIdQuery,
                GetMusicalProjectByIdResult>>();

        var factory = provider.GetRequiredService<IMusicalProjectFactory>();

        Assert.IsType<CreateProjectCommandHandler>(createHandler);
        Assert.IsType<UpdateProjectCommandHandler>(updateHandler);
        Assert.IsType<GetMusicalProjectByIdHandler>(queryHandler);
        Assert.IsType<MusicalProjectFactory>(factory);
    }
}