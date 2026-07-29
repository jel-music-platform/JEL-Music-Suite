namespace JELMusic.Domain.ValueObjects;

public sealed class PerformanceProfile
{
    public string Mood { get; private set; }

    public int TempoBpm { get; private set; }

    public string VocalStyle { get; private set; }

    private PerformanceProfile()
    {
        Mood = string.Empty;
        VocalStyle = string.Empty;
    }

    public PerformanceProfile(
        string mood,
        int tempoBpm,
        string vocalStyle)
    {
        Mood = mood;
        TempoBpm = tempoBpm;
        VocalStyle = vocalStyle;
    }
}