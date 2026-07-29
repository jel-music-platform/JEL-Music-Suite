namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class KnowledgeOrigin
{
    public string Source { get; private set; }

    public string Reference { get; private set; }

    public string CulturalContext { get; private set; }

    public DateTime RegisteredAt { get; private set; }

    private KnowledgeOrigin()
    {
        Source = string.Empty;
        Reference = string.Empty;
        CulturalContext = string.Empty;
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