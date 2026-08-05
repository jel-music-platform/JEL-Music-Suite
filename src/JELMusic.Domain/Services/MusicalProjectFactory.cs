using JELMusic.Domain.Entities;
using JELMusic.Domain.ValueObjects;

namespace JELMusic.Domain.Services;

public sealed class MusicalProjectFactory : IMusicalProjectFactory
{
    public MusicalProject Create(
        string name,
        string genre,
        string description,
        MusicalDNA musicalDNA)
    {
        ArgumentNullException.ThrowIfNull(musicalDNA);

        return MusicalProject.Create(
            name,
            genre,
            description,
            musicalDNA);
    }
}