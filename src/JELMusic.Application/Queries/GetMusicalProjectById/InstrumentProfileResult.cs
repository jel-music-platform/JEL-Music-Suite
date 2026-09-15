namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed record InstrumentProfileResult(
    string Name,
    string Description,
    string CulturalContext,
    KnowledgeOriginResult? Origin,
    string Family,
    string Function,
    string Character);