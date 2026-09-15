using JELMusic.Domain.Enums;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Domain.Tests.ValueObjects;

public class MusicalStyleProfileTests
{
    [Fact]
    public void Constructor_should_preserve_name()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var style = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            origin);

        Assert.Equal("Pop Worship", style.Name);
    }

    [Fact]
    public void Constructor_should_preserve_genre()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var style = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            origin);

        Assert.Equal(MusicalGenre.Pop, style.Genre);
    }

    [Fact]
    public void Constructor_should_preserve_characteristics()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var style = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            origin);

        Assert.Equal("Melodic and accessible", style.Characteristics);
    }

    [Fact]
    public void Constructor_should_preserve_description()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var style = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            origin);

        Assert.Equal("Modern worship style", style.Description);
    }

    [Fact]
    public void Constructor_should_preserve_cultural_context()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var style = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            origin);

        Assert.Equal("Western", style.CulturalContext);
    }

    [Fact]
    public void Constructor_should_preserve_character()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var style = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            origin);

        Assert.Equal("Hopeful", style.Character);
    }

    [Fact]
    public void Constructor_should_preserve_origin()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var style = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            origin);

        Assert.Same(origin, style.Origin);
    }

    [Fact]
    public void Styles_with_same_values_should_be_equal()
    {
        var firstOrigin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var secondOrigin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var first = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            firstOrigin);

        var second = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            secondOrigin);

        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.False(first != second);
    }

    [Fact]
    public void Styles_with_different_genre_should_not_be_equal()
    {
        var first = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful");

        var second = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Worship,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful");

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Styles_with_different_characteristics_should_not_be_equal()
    {
        var first = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful");

        var second = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Rhythmic and energetic",
            "Modern worship style",
            "Western",
            "Hopeful");

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Styles_with_different_character_should_not_be_equal()
    {
        var first = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful");

        var second = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Reflective");

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Styles_with_different_origin_should_not_be_equal()
    {
        var firstOrigin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var secondOrigin = new KnowledgeOrigin(
            "Article",
            "Reference 123",
            "Western");

        var first = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            firstOrigin);

        var second = new MusicalStyleProfile(
            "Pop Worship",
            MusicalGenre.Pop,
            "Melodic and accessible",
            "Modern worship style",
            "Western",
            "Hopeful",
            secondOrigin);

        Assert.NotEqual(first, second);
    }
}
