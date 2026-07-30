using JELMusic.Domain.ValueObjects.MusicalKnowledge;
namespace JELMusic.Domain.ValueObjects.MusicalKnowledge.Core;

public abstract class KnowledgeProfile
{
    public string Name { get; }

    public string Description { get; }

    public string CulturalContext { get;  }

    public KnowledgeOrigin? Origin { get;  }

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