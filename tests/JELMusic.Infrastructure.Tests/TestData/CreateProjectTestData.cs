using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Infrastructure.Tests.TestData;

public static class CreateProjectTestData
{
    public static MusicalDNA CreateDNA()
    {
        return new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Worship",
                74,
                "Tenor"));
    }
}