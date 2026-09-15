using JELMusic.Domain.ValueObjects.MusicalKnowledge;

namespace JELMusic.Domain.Tests.ValueObjects;

public class KnowledgeOriginTests
{
    [Fact]
    public void Constructor_should_preserve_source()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        Assert.Equal("Book", origin.Source);
    }

    [Fact]
    public void Constructor_should_preserve_reference()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        Assert.Equal("Reference 123", origin.Reference);
    }

    [Fact]
    public void Constructor_should_preserve_cultural_context()
    {
        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        Assert.Equal("Western", origin.CulturalContext);
    }

    [Fact]
    public void Constructor_should_set_registered_at()
    {
        var before = DateTime.UtcNow;

        var origin = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var after = DateTime.UtcNow;

        Assert.InRange(origin.RegisteredAt, before, after);
    }

    [Fact]
    public void Origins_with_same_values_should_be_equal()
    {
        var first = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var second = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.False(first != second);
    }

    [Fact]
    public void Origins_with_different_source_should_not_be_equal()
    {
        var first = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var second = new KnowledgeOrigin(
            "Article",
            "Reference 123",
            "Western");

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Origins_with_different_reference_should_not_be_equal()
    {
        var first = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var second = new KnowledgeOrigin(
            "Book",
            "Reference 456",
            "Western");

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Origins_with_different_cultural_context_should_not_be_equal()
    {
        var first = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Western");

        var second = new KnowledgeOrigin(
            "Book",
            "Reference 123",
            "Basque");

        Assert.NotEqual(first, second);
    }
}
