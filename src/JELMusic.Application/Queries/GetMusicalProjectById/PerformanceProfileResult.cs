namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed record PerformanceProfileResult(
    string Mood,
    int TempoBpm,
    string VocalStyle);