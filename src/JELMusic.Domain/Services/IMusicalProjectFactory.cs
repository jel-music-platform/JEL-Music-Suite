using JELMusic.Domain.Entities;
using JELMusic.Domain.ValueObjects;

namespace JELMusic.Domain.Services;

public interface IMusicalProjectFactory
{
    MusicalProject Create(
        string name,
        string genre,
        string description,
        MusicalDNA musicalDNA);
}