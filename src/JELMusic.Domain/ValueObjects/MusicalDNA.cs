using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Domain.ValueObjects;

public sealed class MusicalDNA
{
    public string Influences { get; private set; }

    public string Instruments { get; private set; }

    public string Mood { get; private set; }

    public int TempoBpm { get; private set; }

    public string VocalStyle { get; private set; }

    public MusicalStyleProfile? StyleProfile { get; private set; }

    private MusicalDNA()
    {
        Influences = string.Empty;
        Instruments = string.Empty;
        Mood = string.Empty;
        VocalStyle = string.Empty;
    }

    public MusicalDNA(
        string influences,
        string instruments,
        string mood,
        int tempoBpm,
        string vocalStyle)
    {
        Influences = influences;
        Instruments = instruments;
        Mood = mood;
        TempoBpm = tempoBpm;
        VocalStyle = vocalStyle;
    }
}