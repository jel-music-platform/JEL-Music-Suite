using JELMusic.Domain.Enums;

namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed record MusicalStyleProfileResult(
    string Name,
    MusicalGenre Genre,
    string Characteristics,
    string Description,
    string CulturalContext,
    string Character,
    KnowledgeOriginResult? Origin);