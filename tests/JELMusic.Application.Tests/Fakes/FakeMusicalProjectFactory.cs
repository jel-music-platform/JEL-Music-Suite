using JELMusic.Domain.Entities;
using JELMusic.Domain.Services;
using JELMusic.Domain.ValueObjects;

namespace JELMusic.Application.Tests.Fakes;

public sealed class FakeMusicalProjectFactory : IMusicalProjectFactory
{
    public MusicalProject Create(
        string name,
        string genre,
        string description,
        MusicalDNA musicalDNA)
    {
        return MusicalProject.Create(
            name,
            genre,
            description,
            musicalDNA);
    }
}