using JELMusic.Domain.ValueObjects.MusicalKnowledge;
namespace JELMusic.Domain.ValueObjects;

public sealed class MusicalDNA
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
    }

    public MusicalDNA(
        IEnumerable<InfluenceProfile> influenceProfiles,
        IEnumerable<InstrumentProfile> instrumentProfiles,
        PerformanceProfile performance,
        MusicalStyleProfile? style = null)
    {
        InfluenceProfiles = influenceProfiles.ToList().AsReadOnly();

        InstrumentProfiles = instrumentProfiles.ToList().AsReadOnly();

        Performance = performance;

        Style = style;
    }
}