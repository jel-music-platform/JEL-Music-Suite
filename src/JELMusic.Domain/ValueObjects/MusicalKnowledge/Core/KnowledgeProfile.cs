namespace JELMusic.Domain.ValueObjects.MusicalKnowledge.Core;

public abstract class KnowledgeProfile
{
    public string Name { get; protected set; }

    public string Description { get; protected set; }

    public string CulturalContext { get; protected set; }

    public KnowledgeOrigin? Origin { get; protected set; }

    protected KnowledgeProfile()
    {
        Name = string.Empty;
        Description = string.Empty;
        CulturalContext = string.Empty;
    }

    protected KnowledgeProfile(
        string name,
        string description,
        string culturalContext,
        KnowledgeOrigin? origin)
    {
        Name = name;
        Description = description;
        CulturalContext = culturalContext;
        Origin = origin;
    }
}