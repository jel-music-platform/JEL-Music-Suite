using JELMusic.Domain.Enums;
using JELMusic.Domain.ValueObjects.MusicalKnowledge.Core;

namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class MusicalStyleProfile : KnowledgeProfile
{
    public MusicalGenre Genre { get; }

    public string Characteristics { get; }
    public string Character { get; }

    private MusicalStyleProfile()
    {
        Genre = MusicalGenre.Unknown;
        Characteristics = string.Empty;
        Character = string.Empty;
    }

    public MusicalStyleProfile(
        string name,
        MusicalGenre genre,
        string characteristics,
        string description,
        string culturalContext,
        string character,
        KnowledgeOrigin? origin = null)
        : base(
            name,
            description,
            culturalContext,
            origin)
    {
        Genre = genre;
        Characteristics = characteristics;
        Character = character;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        foreach (var component in GetBaseEqualityComponents())
            yield return component;

        yield return Genre;
        yield return Characteristics;
        yield return Character;
    }
}