using JELMusic.Domain.Common;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Domain.ValueObjects;

public sealed class MusicalDNA : ValueObject
{
    public ICollection<InfluenceProfile> InfluenceProfiles { get; private set; }
    public ICollection<InstrumentProfile> InstrumentProfiles { get; private set; }

    public MusicalStyleProfile? Style { get; }

    public PerformanceProfile Performance { get; }

    private MusicalDNA()
    {
       InfluenceProfiles = new List<InfluenceProfile>();

       InstrumentProfiles = new List<InstrumentProfile>(); 

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

        InfluenceProfiles = influenceProfiles.ToList();

        InstrumentProfiles = instrumentProfiles.ToList();

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