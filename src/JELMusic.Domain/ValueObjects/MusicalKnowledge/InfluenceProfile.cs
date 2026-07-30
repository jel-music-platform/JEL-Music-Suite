using JELMusic.Domain.ValueObjects.MusicalKnowledge.Core;

namespace JELMusic.Domain.ValueObjects.MusicalKnowledge;

public sealed class InfluenceProfile : KnowledgeProfile
{
    public string InfluenceType { get; }

    public string MusicalContribution { get; }


    private InfluenceProfile()
    {
        InfluenceType = string.Empty;
        MusicalContribution = string.Empty;
    }


    public InfluenceProfile(
        string name,
        string influenceType,
        string description,
        string culturalContext,
        string musicalContribution,
        KnowledgeOrigin? origin = null)
        : base(
            name,
            description,
            culturalContext,
            origin)
    {
        InfluenceType = influenceType;
        MusicalContribution = musicalContribution;
    }
}