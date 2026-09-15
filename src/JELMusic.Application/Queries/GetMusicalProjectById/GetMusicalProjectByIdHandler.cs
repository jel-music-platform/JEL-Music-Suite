using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Queries.GetMusicalProjectById;

public sealed class GetMusicalProjectByIdHandler
    : IQueryHandler<GetMusicalProjectByIdQuery, GetMusicalProjectByIdResult>
{
    private readonly IMusicalProjectRepository _repository;

    public GetMusicalProjectByIdHandler(
        IMusicalProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetMusicalProjectByIdResult?> HandleAsync(
        GetMusicalProjectByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var project = await _repository.GetByIdAsync(
            query.ProjectId,
            cancellationToken);

        if (project is null)
        {
            return null;
        }

        var musicalDna = new MusicalDNAResult(
            project.DNA.InfluenceProfiles
                .Select(influence => new InfluenceProfileResult(
                    influence.Name,
                    influence.Description,
                    influence.CulturalContext,
                    influence.Origin is null
                        ? null
                        : new KnowledgeOriginResult(
                            influence.Origin.Source,
                            influence.Origin.Reference,
                            influence.Origin.CulturalContext,
                            influence.Origin.RegisteredAt),
                    influence.InfluenceType,
                    influence.MusicalContribution))
                .ToList(),

            project.DNA.InstrumentProfiles
                .Select(instrument => new InstrumentProfileResult(
                    instrument.Name,
                    instrument.Description,
                    instrument.CulturalContext,
                    instrument.Origin is null
                        ? null
                        : new KnowledgeOriginResult(
                            instrument.Origin.Source,
                            instrument.Origin.Reference,
                            instrument.Origin.CulturalContext,
                            instrument.Origin.RegisteredAt),
                    instrument.Family,
                    instrument.Function,
                    instrument.Character))
                .ToList(),

            project.DNA.Style is null
                ? null
                : new MusicalStyleProfileResult(
                    project.DNA.Style.Name,
                    project.DNA.Style.Genre,
                    project.DNA.Style.Characteristics,
                    project.DNA.Style.Description,
                    project.DNA.Style.CulturalContext,
                    project.DNA.Style.Character,
                    project.DNA.Style.Origin is null
                        ? null
                        : new KnowledgeOriginResult(
                            project.DNA.Style.Origin.Source,
                            project.DNA.Style.Origin.Reference,
                            project.DNA.Style.Origin.CulturalContext,
                            project.DNA.Style.Origin.RegisteredAt)),

            new PerformanceProfileResult(
                project.DNA.Performance.Mood,
                project.DNA.Performance.TempoBpm,
                project.DNA.Performance.VocalStyle));

        return new GetMusicalProjectByIdResult(
            project.Id,
            project.Name,
            project.Genre,
            project.Description,
            musicalDna);
    }
}
