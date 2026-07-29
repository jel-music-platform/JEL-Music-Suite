using JELMusic.Domain.ValueObjects.MusicalKnowledge.Core;

namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class InstrumentProfile : KnowledgeProfile
{
    public string Family { get; private set; }

    public string Function { get; private set; }

    public string Character { get; private set; }

    private InstrumentProfile()
    {
        Family = string.Empty;
        Function = string.Empty;
        Character = string.Empty;
    }

    public InstrumentProfile(
        string name,
        string family,
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
        Family = family;
        Function = description;
        Character = character;
    }
}