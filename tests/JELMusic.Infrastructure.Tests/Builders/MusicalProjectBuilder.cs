using JELMusic.Domain.Entities;
using JELMusic.Domain.ValueObjects;
using JELMusic.Domain.ValueObjects.MusicalKnowledge;
using JELMusic.Domain.Enums;

namespace JELMusic.Infrastructure.Tests.Builders;

public static class MusicalProjectBuilder
{
    public static MusicalProject Create()
    {
        var origin = new KnowledgeOrigin(
            "Investigación",
            "Cancionero Vasco",
            "Euskadi");

        var influence = new InfluenceProfile(
            name: "Folk Vasco",
            influenceType: "Tradición",
            description: "Tradición musical vasca",
            culturalContext: "Euskadi",
            musicalContribution: "Influencia rítmica",
            origin: origin);

        var instrument = new InstrumentProfile(
            name: "Trikitixa",
            family: "Acordeón diatónico",
            function: "Principal",
            description: "Instrumento tradicional vasco",
            culturalContext: "Euskadi",
            character: "Melódico",
            origin: origin);

        var style = new MusicalStyleProfile(
            name: "Folk Vasco",
            genre: MusicalGenre.Folk,
            characteristics: "Ritmos tradicionales",
            description: "Estilo tradicional vasco",
            culturalContext: "Euskadi",
            character: "Popular",
            origin: origin);

        var performance = new PerformanceProfile(
            mood: "Épico",
            tempoBpm: 90,
            vocalStyle: "Masculina");

        var dna = new MusicalDNA(
            new[] { influence },
            new[] { instrument },
            performance,
            style);

       return MusicalProject.Create(
           name: "Proyecto Test",
           genre: "Folk",
           description: "Proyecto de integraciÃ³n",        
           dna: dna);
        
    }
}