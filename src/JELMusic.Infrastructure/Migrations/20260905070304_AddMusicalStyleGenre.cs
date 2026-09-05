using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JELMusic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicalStyleGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StyleGenre",
                table: "MusicalProjects",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StyleGenre",
                table: "MusicalProjects");
        }
    }
}
