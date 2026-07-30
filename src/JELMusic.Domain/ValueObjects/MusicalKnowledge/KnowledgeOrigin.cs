namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class KnowledgeOrigin
{
    public string Source { get; }

    public string Reference { get; }

    public string CulturalContext { get; }

    public DateTime RegisteredAt { get; }

    private KnowledgeOrigin()
    {
        Source = string.Empty;
        Reference = string.Empty;
        CulturalContext = string.Empty;
        RegisteredAt = DateTime.UtcNow;
    }

    public KnowledgeOrigin(
        string source,
        string reference,
        string culturalContext)
    {
        Source = source;
        Reference = reference;
        CulturalContext = culturalContext;
        RegisteredAt = DateTime.UtcNow;
    }
}