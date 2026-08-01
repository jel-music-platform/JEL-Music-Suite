using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JELMusic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCoreSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicalProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Genre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    StyleCharacteristics = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    StyleCharacter = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    StyleName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    StyleDescription = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    StyleCulturalContext = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    StyleOriginSource = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    StyleOriginReference = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    StyleOriginCulturalContext = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    StyleOriginRegisteredAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Mood = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TempoBpm = table.Column<int>(type: "INTEGER", nullable: false),
                    VocalStyle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicalProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfluenceProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InfluenceType = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    MusicalContribution = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    MusicalDNAMusicalProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    CulturalContext = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    OriginSource = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    OriginReference = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    OriginCulturalContext = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    OriginRegisteredAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfluenceProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfluenceProfile_MusicalProjects_MusicalDNAMusicalProjectId",
                        column: x => x.MusicalDNAMusicalProjectId,
                        principalTable: "MusicalProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Family = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Function = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Character = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    MusicalDNAMusicalProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    CulturalContext = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    OriginSource = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    OriginReference = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    OriginCulturalContext = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    OriginRegisteredAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentProfile_MusicalProjects_MusicalDNAMusicalProjectId",
                        column: x => x.MusicalDNAMusicalProjectId,
                        principalTable: "MusicalProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfluenceProfile_MusicalDNAMusicalProjectId",
                table: "InfluenceProfile",
                column: "MusicalDNAMusicalProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentProfile_MusicalDNAMusicalProjectId",
                table: "InstrumentProfile",
                column: "MusicalDNAMusicalProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfluenceProfile");

            migrationBuilder.DropTable(
                name: "InstrumentProfile");

            migrationBuilder.DropTable(
                name: "MusicalProjects");
        }
    }
}
