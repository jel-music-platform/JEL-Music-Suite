namespace JELMusic.Domain.ValueObjects;

public sealed class PerformanceProfile
{
    public string Mood { get; }

    public int TempoBpm { get; }

    public string VocalStyle { get; }


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