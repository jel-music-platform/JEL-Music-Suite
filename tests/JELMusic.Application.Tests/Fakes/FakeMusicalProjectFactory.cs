using JELMusic.Domain.Entities;
using JELMusic.Domain.Services;
using JELMusic.Domain.ValueObjects;

namespace JELMusic.Application.Tests.Fakes;

public sealed class FakeMusicalProjectFactory : IMusicalProjectFactory
{
    public int CreateCallCount { get; private set; }

    public MusicalProject? CreatedProject { get; private set; }

    public MusicalProject Create(
        string name,
        string genre,
        string description,
        MusicalDNA musicalDNA)
    {
        CreateCallCount++;

        CreatedProject = MusicalProject.Create(
            name,
            genre,
            description,
            musicalDNA);

        return CreatedProject;
    }
}
