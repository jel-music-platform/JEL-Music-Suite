namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class InfluenceProfile
{
    public string Name { get; private set; }

    public string Origin { get; private set; }

    public string Characteristics { get; private set; }

    public string CulturalContext { get; private set; }

    public string Character { get; private set; }

    private InfluenceProfile()
    {
        Name = string.Empty;
        Origin = string.Empty;
        Characteristics = string.Empty;
        CulturalContext = string.Empty;
        Character = string.Empty;
    }

    public InfluenceProfile(
        string name,
        string origin,
        string characteristics,
        string culturalContext,
        string character)
    {
        Name = name;
        Origin = origin;
        Characteristics = characteristics;
        CulturalContext = culturalContext;
        Character = character;
    }
}