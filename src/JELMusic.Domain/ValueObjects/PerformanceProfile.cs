using JELMusic.Domain.Common;

namespace JELMusic.Domain.ValueObjects;

public sealed class PerformanceProfile : ValueObject
{
    public string Mood { get; }

    public int TempoBpm { get; }

    public string VocalStyle { get; }

    private PerformanceProfile()
    {
        Mood = string.Empty;
        TempoBpm = 60;
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

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Mood;
        yield return TempoBpm;
        yield return VocalStyle;
    }
}