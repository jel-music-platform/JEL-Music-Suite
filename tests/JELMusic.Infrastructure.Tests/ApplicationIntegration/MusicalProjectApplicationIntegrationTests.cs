using JELMusic.Application;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Projects.CreateProject;
using JELMusic.Application.Projects.UpdateProject;
using JELMusic.Application.Queries.GetMusicalProjectById;
using JELMusic.Application.Queries.ListMusicalProjects;
using JELMusic.Domain.Enums;
using JELMusic.Domain.Repositories;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;
using JELMusic.Framework;
using JELMusic.Infrastructure.Persistence;
using JELMusic.Infrastructure.Persistence.Repositories;
using JELMusic.Infrastructure.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Infrastructure.Tests.ApplicationIntegration;

public sealed class MusicalProjectApplicationIntegrationTests
{
    [Fact]
    public async Task Create_then_get_should_persist_project()
    {
        using var database = new SqliteTestDatabase();
        using var serviceProvider = CreateServiceProvider(database);

        using var scope = serviceProvider.CreateScope();

        var dispatcher = scope.ServiceProvider
            .GetRequiredService<IApplicationDispatcher>();

        var command = new CreateProjectCommand(
            "Proyecto integraci\u00F3n",
            "Worship",
            "Descripci\u00F3n de integraci\u00F3n",
            CreateMusicalDNA());

        var projectId = await dispatcher.SendCommandAsync<
            CreateProjectCommand,
            Guid>(command);

        var result = await dispatcher.SendQueryAsync<
            GetMusicalProjectByIdQuery,
            GetMusicalProjectByIdResult>(
            new GetMusicalProjectByIdQuery(projectId));

        Assert.NotNull(result);
        Assert.Equal(projectId, result!.ProjectId);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.Genre, result.Genre);
        Assert.Equal(command.Description, result.Description);
    }

    [Fact]
    public async Task Update_then_get_should_persist_changes()
    {
        using var database = new SqliteTestDatabase();
        using var serviceProvider = CreateServiceProvider(database);

        using var scope = serviceProvider.CreateScope();

        var dispatcher = scope.ServiceProvider
            .GetRequiredService<IApplicationDispatcher>();

        var dna = CreateMusicalDNA();

        var projectId = await dispatcher.SendCommandAsync<
            CreateProjectCommand,
            Guid>(
            new CreateProjectCommand(
                "Nombre original",
                "Pop",
                "Descripci\u00F3n original",
                dna));

        var updateResult = await dispatcher.SendCommandAsync<
            UpdateProjectCommand,
            UpdateProjectResult>(
            new UpdateProjectCommand(
                projectId,
                "Nombre actualizado",
                "Worship",
                "Descripci\u00F3n actualizada",
                dna));

        Assert.Equal(projectId, updateResult.ProjectId);
        Assert.Equal(2, updateResult.ProjectVersion);

        database.Context.ChangeTracker.Clear();

        var result = await dispatcher.SendQueryAsync<
            GetMusicalProjectByIdQuery,
            GetMusicalProjectByIdResult>(
            new GetMusicalProjectByIdQuery(projectId));

        Assert.NotNull(result);
        Assert.Equal(projectId, result!.ProjectId);
        Assert.Equal("Nombre actualizado", result.Name);
        Assert.Equal("Worship", result.Genre);
        Assert.Equal(
            "Descripci\u00F3n actualizada",
            result.Description);
    }

    [Fact]
    public async Task List_should_return_persisted_projects()
    {
        using var database = new SqliteTestDatabase();
        using var serviceProvider = CreateServiceProvider(database);

        using var scope = serviceProvider.CreateScope();

        var dispatcher = scope.ServiceProvider
            .GetRequiredService<IApplicationDispatcher>();

        await dispatcher.SendCommandAsync<
            CreateProjectCommand,
            Guid>(
            new CreateProjectCommand(
                "Proyecto uno",
                "Pop",
                "Descripci\u00F3n uno",
                CreateMusicalDNA()));

        await dispatcher.SendCommandAsync<
            CreateProjectCommand,
            Guid>(
            new CreateProjectCommand(
                "Proyecto dos",
                "Folk",
                "Descripci\u00F3n dos",
                CreateMusicalDNA()));

        var result = await dispatcher.SendQueryAsync<
            ListMusicalProjectsQuery,
            ListMusicalProjectsResult>(
            new ListMusicalProjectsQuery());

        Assert.NotNull(result);
        Assert.Equal(2, result!.Projects.Count);
        Assert.Contains(
            result.Projects,
            project => project.Name == "Proyecto uno");
        Assert.Contains(
            result.Projects,
            project => project.Name == "Proyecto dos");
    }

    [Fact]
    public async Task Create_update_and_list_should_persist_complete_musical_dna()
    {
        using var database = new SqliteTestDatabase();
        using var serviceProvider = CreateServiceProvider(database);

        using var scope = serviceProvider.CreateScope();

        var dispatcher = scope.ServiceProvider
            .GetRequiredService<IApplicationDispatcher>();

        var originalDna = CreateRichMusicalDNA(
            "Folk Vasco",
            MusicalGenre.Folk,
            "Trikitixa",
            "Estilo folk de ra\u00EDz vasca");

        var projectId = await dispatcher.SendCommandAsync<
            CreateProjectCommand,
            Guid>(
            new CreateProjectCommand(
                "Proyecto DNA completo",
                "Folk",
                "Proyecto con ADN musical completo",
                originalDna));

        var created = await dispatcher.SendQueryAsync<
            GetMusicalProjectByIdQuery,
            GetMusicalProjectByIdResult>(
            new GetMusicalProjectByIdQuery(projectId));

        Assert.NotNull(created);
        Assert.Equal(projectId, created!.ProjectId);
        Assert.Equal("Proyecto DNA completo", created.Name);
        Assert.Equal("Folk", created.Genre);
        Assert.Equal(
            "Proyecto con ADN musical completo",
            created.Description);

        Assert.Single(created.MusicalDNA.InfluenceProfiles);
        var createdInfluence =
            created.MusicalDNA.InfluenceProfiles[0];

        Assert.Equal("Folk Vasco", createdInfluence.Name);
        Assert.Equal(
            "Influencia tradicional vasca",
            createdInfluence.Description);
        Assert.Equal(
            "Tradici\u00F3n vasca",
            createdInfluence.CulturalContext);
        Assert.Equal(
            "Cultural",
            createdInfluence.InfluenceType);
        Assert.Equal(
            "Aporta identidad mel\u00F3dica y r\u00EDtmica",
            createdInfluence.MusicalContribution);

        Assert.NotNull(createdInfluence.Origin);
        Assert.Equal(
            "Archivo musical",
            createdInfluence.Origin!.Source);
        Assert.Equal(
            "REF-001",
            createdInfluence.Origin.Reference);
        Assert.Equal(
            "Tradici\u00F3n vasca",
            createdInfluence.Origin.CulturalContext);

        Assert.Single(created.MusicalDNA.InstrumentProfiles);
        var createdInstrument =
            created.MusicalDNA.InstrumentProfiles[0];

        Assert.Equal("Trikitixa", createdInstrument.Name);
        Assert.Equal(
            "Acorde\u00F3n",
            createdInstrument.Family);
        Assert.Equal(
            "Base arm\u00F3nica y r\u00EDtmica",
            createdInstrument.Function);
        Assert.Equal(
            "Instrumento tradicional vasco",
            createdInstrument.Description);
        Assert.Equal(
            "M\u00FAsica vasca",
            createdInstrument.CulturalContext);
        Assert.Equal(
            "C\u00E1lido y mel\u00F3dico",
            createdInstrument.Character);

        Assert.NotNull(createdInstrument.Origin);
        Assert.Equal(
            "Investigaci\u00F3n propia",
            createdInstrument.Origin!.Source);
        Assert.Equal(
            "REF-002",
            createdInstrument.Origin.Reference);
        Assert.Equal(
            "M\u00FAsica vasca",
            createdInstrument.Origin.CulturalContext);

        Assert.NotNull(created.MusicalDNA.Style);
        var createdStyle = created.MusicalDNA.Style!;

        Assert.Equal("Folk Vasco", createdStyle.Name);
        Assert.Equal(
            MusicalGenre.Folk,
            createdStyle.Genre);
        Assert.Equal(
            "Tradicional, org\u00E1nico y mel\u00F3dico",
            createdStyle.Characteristics);
        Assert.Equal(
            "Estilo folk de ra\u00EDz vasca",
            createdStyle.Description);
        Assert.Equal(
            "Tradici\u00F3n musical vasca",
            createdStyle.CulturalContext);
        Assert.Equal(
            "Org\u00E1nico y cercano",
            createdStyle.Character);

        Assert.NotNull(createdStyle.Origin);
        Assert.Equal(
            "Biblioteca musical",
            createdStyle.Origin!.Source);
        Assert.Equal(
            "REF-003",
            createdStyle.Origin.Reference);
        Assert.Equal(
            "Cultura vasca",
            createdStyle.Origin.CulturalContext);

        Assert.Equal(
            "Worship",
            created.MusicalDNA.Performance.Mood);
        Assert.Equal(
            74,
            created.MusicalDNA.Performance.TempoBpm);
        Assert.Equal(
            "Tenor",
            created.MusicalDNA.Performance.VocalStyle);

        var updatedDna = CreateRichMusicalDNA(
            "Folk Irland\u00E9s",
            MusicalGenre.Folk,
            "Irish Fiddle",
            "Folk irland\u00E9s");

        var updateResult = await dispatcher.SendCommandAsync<
            UpdateProjectCommand,
            UpdateProjectResult>(
            new UpdateProjectCommand(
                projectId,
                "Proyecto DNA actualizado",
                "Folk",
                "Descripci\u00F3n DNA actualizada",
                updatedDna));

        Assert.Equal(projectId, updateResult.ProjectId);
        Assert.Equal(2, updateResult.ProjectVersion);

        database.Context.ChangeTracker.Clear();

        var updated = await dispatcher.SendQueryAsync<
            GetMusicalProjectByIdQuery,
            GetMusicalProjectByIdResult>(
            new GetMusicalProjectByIdQuery(projectId));

        Assert.NotNull(updated);
        Assert.Equal(projectId, updated!.ProjectId);
        Assert.Equal("Proyecto DNA actualizado", updated.Name);
        Assert.Equal("Folk", updated.Genre);
        Assert.Equal(
            "Descripci\u00F3n DNA actualizada",
            updated.Description);

        Assert.Single(updated.MusicalDNA.InfluenceProfiles);
        Assert.Equal(
            "Folk Irland\u00E9s",
            updated.MusicalDNA.InfluenceProfiles[0].Name);

        Assert.Single(updated.MusicalDNA.InstrumentProfiles);
        Assert.Equal(
            "Irish Fiddle",
            updated.MusicalDNA.InstrumentProfiles[0].Name);

        Assert.NotNull(updated.MusicalDNA.Style);
        Assert.Equal(
            "Folk irland\u00E9s",
            updated.MusicalDNA.Style!.Description);

        Assert.Equal(
            "Worship",
            updated.MusicalDNA.Performance.Mood);
        Assert.Equal(
            74,
            updated.MusicalDNA.Performance.TempoBpm);
        Assert.Equal(
            "Tenor",
            updated.MusicalDNA.Performance.VocalStyle);

        var listed = await dispatcher.SendQueryAsync<
            ListMusicalProjectsQuery,
            ListMusicalProjectsResult>(
            new ListMusicalProjectsQuery());

        Assert.NotNull(listed);
        Assert.Single(listed!.Projects);
        Assert.Equal(
            projectId,
            listed.Projects[0].ProjectId);
        Assert.Equal(
            "Proyecto DNA actualizado",
            listed.Projects[0].Name);
    }

    private static ServiceProvider CreateServiceProvider(
        SqliteTestDatabase database)
    {
        var services = new ServiceCollection();

        services.AddApplication();
        services.AddJELMusicFramework();

        services.AddScoped(_ => database.Context);

        services.AddScoped<
            IMusicalProjectRepository,
            MusicalProjectRepository>();

        services.AddScoped<
            IUnitOfWork,
            CoreUnitOfWork>();

        return services.BuildServiceProvider();
    }

    private static MusicalDNA CreateMusicalDNA()
    {
        return new MusicalDNA(
            Array.Empty<InfluenceProfile>(),
            Array.Empty<InstrumentProfile>(),
            new PerformanceProfile(
                "Neutral",
                120,
                "Instrumental"));
    }

    private static MusicalDNA CreateRichMusicalDNA(
        string influenceName,
        MusicalGenre genre,
        string instrumentName,
        string styleDescription)
    {
        var influenceOrigin = new KnowledgeOrigin(
            "Archivo musical",
            "REF-001",
            "Tradici\u00F3n vasca");

        var instrumentOrigin = new KnowledgeOrigin(
            "Investigaci\u00F3n propia",
            "REF-002",
            "M\u00FAsica vasca");

        var styleOrigin = new KnowledgeOrigin(
            "Biblioteca musical",
            "REF-003",
            "Cultura vasca");

        var influence = new InfluenceProfile(
            influenceName,
            "Cultural",
            "Influencia tradicional vasca",
            "Tradici\u00F3n vasca",
            "Aporta identidad mel\u00F3dica y r\u00EDtmica",
            influenceOrigin);

        var instrument = new InstrumentProfile(
            instrumentName,
            instrumentName == "Trikitixa"
                ? "Acorde\u00F3n"
                : "Cuerda frotada",
            instrumentName == "Trikitixa"
                ? "Base arm\u00F3nica y r\u00EDtmica"
                : "Melod\u00EDa principal",
            instrumentName == "Trikitixa"
                ? "Instrumento tradicional vasco"
                : "Instrumento tradicional irland\u00E9s",
            instrumentName == "Trikitixa"
                ? "M\u00FAsica vasca"
                : "M\u00FAsica irlandesa",
            instrumentName == "Trikitixa"
                ? "C\u00E1lido y mel\u00F3dico"
                : "Vivo y expresivo",
            instrumentOrigin);

        var style = new MusicalStyleProfile(
            instrumentName == "Trikitixa"
                ? "Folk Vasco"
                : "Folk Irland\u00E9s",
            genre,
            instrumentName == "Trikitixa"
                ? "Tradicional, org\u00E1nico y mel\u00F3dico"
                : "Tradicional, org\u00E1nico y festivo",
            styleDescription,
            instrumentName == "Trikitixa"
                ? "Tradici\u00F3n musical vasca"
                : "Tradici\u00F3n musical irlandesa",
            instrumentName == "Trikitixa"
                ? "Org\u00E1nico y cercano"
                : "Vivo y festivo",
            styleOrigin);

        return new MusicalDNA(
            new[] { influence },
            new[] { instrument },
            new PerformanceProfile(
                "Worship",
                74,
                "Tenor"),
            style);
    }
}
