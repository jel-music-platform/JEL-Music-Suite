namespace JELMusic.Domain.ValueObjects;

public sealed class MusicalDNA
{
    public string Influences { get; private set; }

    public string Instruments { get; private set; }

    public MusicalKnowledge.MusicalStyleProfile? Style { get; private set; }

    public PerformanceProfile Performance { get; private set; }

    private MusicalDNA()
    {
        Influences = string.Empty;
        Instruments = string.Empty;

        Performance = new PerformanceProfile(
            string.Empty,
            0,
            string.Empty);
    }

    public MusicalDNA(
        string influences,
        string instruments,
        PerformanceProfile performance,
        MusicalKnowledge.MusicalStyleProfile? style = null)
    {
        Influences = influences;
        Instruments = instruments;
        Performance = performance;
        Style = style;
    }
}