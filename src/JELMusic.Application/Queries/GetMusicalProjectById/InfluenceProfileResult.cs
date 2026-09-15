namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed record InfluenceProfileResult(
    string Name,
    string Description,
    string CulturalContext,
    KnowledgeOriginResult? Origin,
    string InfluenceType,
    string MusicalContribution);