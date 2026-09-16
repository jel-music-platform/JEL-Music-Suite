using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Entities;

namespace JELMusic.Application.VideoProjects.CreateVideoProject;

public sealed class CreateVideoProjectCommandHandler
    : ICommandHandler<CreateVideoProjectCommand, Guid>
{
    public Task<Guid> HandleAsync(
        CreateVideoProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var videoProject = VideoProject.Create(
            command.MusicalProjectId,
            command.Name,
            command.Concept,
            command.Duration);

        return Task.FromResult(videoProject.Id);
    }
}
