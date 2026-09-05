using JELMusic.Application;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Projects.UpdateProject;
using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Application.Tests.Fakes;
using JELMusic.Domain.Repositories;
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
        using var scope = provider.CreateScope();

        var createHandler = scope.ServiceProvider.GetRequiredService<
            ICommandHandler<CreateProjectCommand, Guid>>();

        var updateHandler = scope.ServiceProvider.GetRequiredService<
            ICommandHandler<UpdateProjectCommand, UpdateProjectResult>>();

        var queryHandler = scope.ServiceProvider.GetRequiredService<
            IQueryHandler<
                GetMusicalProjectByIdQuery,
                GetMusicalProjectByIdResult>>();

        var createHandlerAgain = scope.ServiceProvider.GetRequiredService<
            ICommandHandler<CreateProjectCommand, Guid>>();

        var updateHandlerAgain = scope.ServiceProvider.GetRequiredService<
            ICommandHandler<UpdateProjectCommand, UpdateProjectResult>>();

        var queryHandlerAgain = scope.ServiceProvider.GetRequiredService<
            IQueryHandler<
                GetMusicalProjectByIdQuery,
                GetMusicalProjectByIdResult>>();

        Assert.IsType<CreateProjectCommandHandler>(createHandler);
        Assert.IsType<UpdateProjectCommandHandler>(updateHandler);
        Assert.IsType<GetMusicalProjectByIdHandler>(queryHandler);

        Assert.Same(createHandler, createHandlerAgain);
        Assert.Same(updateHandler, updateHandlerAgain);
        Assert.Same(queryHandler, queryHandlerAgain);
    }
}