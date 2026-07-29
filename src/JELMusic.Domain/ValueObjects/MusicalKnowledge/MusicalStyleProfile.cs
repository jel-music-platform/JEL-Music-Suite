namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class MusicalStyleProfile
{
    public string Name { get; private set; }

    public string Characteristics { get; private set; }

    public string CulturalContext { get; private set; }

    public string Character { get; private set; }

    private MusicalStyleProfile()
    {
        Name = string.Empty;
        Characteristics = string.Empty;
        CulturalContext = string.Empty;
        Character = string.Empty;
    }

    public MusicalStyleProfile(
        string name,
        string characteristics,
        string culturalContext,
        string character)
    {
        Name = name;
        Characteristics = characteristics;
        CulturalContext = culturalContext;
        Character = character;
    }
}