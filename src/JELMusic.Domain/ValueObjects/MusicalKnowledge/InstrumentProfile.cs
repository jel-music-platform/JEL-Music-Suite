namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class InstrumentProfile
{
    public string Name { get; private set; }

    public string Family { get; private set; }

    public string Origin { get; private set; }

    public string Function { get; private set; }

    public string Character { get; private set; }

    private InstrumentProfile()
    {
        Name = string.Empty;
        Family = string.Empty;
        Origin = string.Empty;
        Function = string.Empty;
        Character = string.Empty;
    }

    public InstrumentProfile(
        string name,
        string family,
        string origin,
        string function,
        string character)
    {
        Name = name;
        Family = family;
        Origin = origin;
        Function = function;
        Character = character;
    }
}