using JELMusic.Domain.ValueObjects.MusicalKnowledge.Core;

namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class InstrumentProfile : KnowledgeProfile
{
    public string Family { get; }

    public string Function { get; }

    public string Character { get; }

    private InstrumentProfile()
    {
        Family = string.Empty;
        Function = string.Empty;
        Character = string.Empty;
    }

    public InstrumentProfile(
        string name,
        string family,
        string function,
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
        Function = function;
        Character = character;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        foreach (var component in GetBaseEqualityComponents())
            yield return component;

        yield return Family;
        yield return Function;
        yield return Character;
    }
}