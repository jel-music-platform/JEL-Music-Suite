using JELMusic.Domain.Common;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Domain.ValueObjects;

public sealed class MusicalDNA : ValueObject
{
    public IReadOnlyCollection<InfluenceProfile> InfluenceProfiles { get; }

    public IReadOnlyCollection<InstrumentProfile> InstrumentProfiles { get; }

    public MusicalStyleProfile? Style { get; }

    public PerformanceProfile Performance { get; }

    private MusicalDNA()
    {
        InfluenceProfiles = Array.Empty<InfluenceProfile>();

        InstrumentProfiles = Array.Empty<InstrumentProfile>();

        Performance = new PerformanceProfile(
            string.Empty,
            60,
            string.Empty);

        Style = null;
    }

    public MusicalDNA(
        IEnumerable<InfluenceProfile> influenceProfiles,
        IEnumerable<InstrumentProfile> instrumentProfiles,
        PerformanceProfile performance,
        MusicalStyleProfile? style = null)
    {
        ArgumentNullException.ThrowIfNull(influenceProfiles);
        ArgumentNullException.ThrowIfNull(instrumentProfiles);
        ArgumentNullException.ThrowIfNull(performance);

        InfluenceProfiles = influenceProfiles
            .ToList()
            .AsReadOnly();

        InstrumentProfiles = instrumentProfiles
            .ToList()
            .AsReadOnly();

        Performance = performance;

        Style = style;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        foreach (var influence in InfluenceProfiles)
            yield return influence;

        foreach (var instrument in InstrumentProfiles)
            yield return instrument;

        yield return Style;
        yield return Performance;
    }
}