using JELMusic.Domain.ValueObjects.MusicalKnowledge.Core;

namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class MusicalStyleProfile : KnowledgeProfile
{
    public string Characteristics { get; private set; }

    public string Character { get; private set; }

    private MusicalStyleProfile()
    {
        Characteristics = string.Empty;
        Character = string.Empty;
    }

    public MusicalStyleProfile(
        string name,
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
        Characteristics = characteristics;
        Character = character;
    }
}