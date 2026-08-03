using JELMusic.Application.Abstractions.Dispatching;

namespace JELMusic.Application.Projects.CreateProject;

public sealed class CreateProjectCommandHandler
    : ICommandHandler<CreateProjectCommand, Guid>
{
    public Task<Guid> HandleAsync(
        CreateProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        // Implementación temporal.
        return Task.FromResult(Guid.NewGuid());
    }
}