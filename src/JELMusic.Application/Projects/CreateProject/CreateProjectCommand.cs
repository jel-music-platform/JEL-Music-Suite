using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.ValueObjects;

namespace JELMusic.Application.Projects.CreateProject;

public sealed record CreateProjectCommand(
    string Name,
    MusicalDNA MusicalDNA)
    : ICommand<Guid>;