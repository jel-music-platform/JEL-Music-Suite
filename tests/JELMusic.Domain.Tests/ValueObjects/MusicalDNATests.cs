using JELMusic.Domain.Enums;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Domain.Tests.ValueObjects;

public class MusicalDNATests
{
    [Fact]
    public void Constructor_should_create_dna_with_empty_collections()
    {
        var performance = new PerformanceProfile(
            "Neutral",
            120,
            "Instrumental");

        var dna = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            performance);

        Assert.NotNull(dna.InfluenceProfiles);
        Assert.Empty(dna.InfluenceProfiles);

        Assert.NotNull(dna.InstrumentProfiles);
        Assert.Empty(dna.InstrumentProfiles);

        Assert.Same(performance, dna.Performance);
        Assert.Null(dna.Style);
    }

    [Fact]
    public void Constructor_should_copy_influence_profiles()
    {
        var influences = new List<InfluenceProfile>();

        var dna = new MusicalDNA(
            influences,
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Neutral",
                120,
                "Instrumental"));

        influences.Add(
            new InfluenceProfile(
                "Jazz",
                "Jazz",
                "Jazz influence",
                "United States",
                "Harmonic and melodic influence"));

        Assert.Empty(dna.InfluenceProfiles);
    }

    [Fact]
    public void Constructor_should_copy_instrument_profiles()
    {
        var instruments = new List<InstrumentProfile>();

        var dna = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            instruments,
            new PerformanceProfile(
                "Neutral",
                120,
                "Instrumental"));

        instruments.Add(
            new InstrumentProfile(
                "Piano",
                "Keyboard",
                "Harmony",
                "Acoustic piano",
                "Western",
                "Acoustic"));

        Assert.Empty(dna.InstrumentProfiles);
    }

    [Fact]
    public void Constructor_should_preserve_style()
    {
        var style = new MusicalStyleProfile(
            "Pop",
            MusicalGenre.Pop,
            "Contemporary",
            "Contemporary pop style",
            "Western",
            "Melodic");

        var dna = new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Energetic",
                128,
                "Lead Vocal"),
            style);

        Assert.Same(style, dna.Style);
    }

    [Fact]
    public void Constructor_should_throw_when_influence_profiles_are_null()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => new MusicalDNA(
                null!,
                Array.Empty<InstrumentProfile>(),
                new PerformanceProfile(
                    "Neutral",
                    120,
                    "Instrumental")));

        Assert.Equal("influenceProfiles", exception.ParamName);
    }

    [Fact]
    public void Constructor_should_throw_when_instrument_profiles_are_null()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => new MusicalDNA(
                Array.Empty<InfluenceProfile>(),
                null!,
                new PerformanceProfile(
                    "Neutral",
                    120,
                    "Instrumental")));

        Assert.Equal("instrumentProfiles", exception.ParamName);
    }

    [Fact]
    public void Constructor_should_throw_when_performance_is_null()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => new MusicalDNA(
                Array.Empty<InfluenceProfile>(),
                Array.Empty<InstrumentProfile>(),
                null!));

        Assert.Equal("performance", exception.ParamName);
    }
}
