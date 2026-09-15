namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed record KnowledgeOriginResult(
    string Source,
    string Reference,
    string CulturalContext,
    DateTime RegisteredAt);