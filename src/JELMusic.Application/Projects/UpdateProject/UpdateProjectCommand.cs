using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.ValueObjects;

namespace JELMusic.Application.Projects.UpdateProject;

public sealed record UpdateProjectCommand(
    Guid ProjectId,
    string Name,
    string Genre,
    string Description,
    MusicalDNA MusicalDNA)
    : ICommand<UpdateProjectResult>;