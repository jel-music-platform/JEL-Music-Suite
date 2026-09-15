using JELMusic.Domain.ValueObjects;

namespace JELMusic.Domain.Tests.ValueObjects;

public class PerformanceProfileTests
{
    [Fact]
    public void Constructor_should_preserve_mood()
    {
        var profile = new PerformanceProfile(
            "Energetic",
            128,
            "Lead Vocal");

        Assert.Equal("Energetic", profile.Mood);
    }

    [Fact]
    public void Constructor_should_preserve_tempo()
    {
        var profile = new PerformanceProfile(
            "Energetic",
            128,
            "Lead Vocal");

        Assert.Equal(128, profile.TempoBpm);
    }

    [Fact]
    public void Constructor_should_preserve_vocal_style()
    {
        var profile = new PerformanceProfile(
            "Energetic",
            128,
            "Lead Vocal");

        Assert.Equal("Lead Vocal", profile.VocalStyle);
    }

    [Fact]
    public void Profiles_with_same_values_should_be_equal()
    {
        var first = new PerformanceProfile(
            "Energetic",
            128,
            "Lead Vocal");

        var second = new PerformanceProfile(
            "Energetic",
            128,
            "Lead Vocal");

        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.False(first != second);
    }

    [Fact]
    public void Profiles_with_different_mood_should_not_be_equal()
    {
        var first = new PerformanceProfile(
            "Energetic",
            128,
            "Lead Vocal");

        var second = new PerformanceProfile(
            "Calm",
            128,
            "Lead Vocal");

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Profiles_with_different_tempo_should_not_be_equal()
    {
        var first = new PerformanceProfile(
            "Energetic",
            128,
            "Lead Vocal");

        var second = new PerformanceProfile(
            "Energetic",
            120,
            "Lead Vocal");

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Profiles_with_different_vocal_style_should_not_be_equal()
    {
        var first = new PerformanceProfile(
            "Energetic",
            128,
            "Lead Vocal");

        var second = new PerformanceProfile(
            "Energetic",
            128,
            "Instrumental");

        Assert.NotEqual(first, second);
    }
}
