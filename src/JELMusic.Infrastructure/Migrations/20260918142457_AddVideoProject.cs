using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JELMusic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MusicalProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Concept = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoProjects_MusicalProjects_MusicalProjectId",
                        column: x => x.MusicalProjectId,
                        principalTable: "MusicalProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoProjects_MusicalProjectId",
                table: "VideoProjects",
                column: "MusicalProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoProjects");
        }
    }
}
