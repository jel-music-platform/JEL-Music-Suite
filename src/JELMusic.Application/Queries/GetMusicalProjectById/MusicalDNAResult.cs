namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed record MusicalDNAResult(
    IReadOnlyList<InfluenceProfileResult> InfluenceProfiles,
    IReadOnlyList<InstrumentProfileResult> InstrumentProfiles,
    MusicalStyleProfileResult? Style,
    PerformanceProfileResult Performance);