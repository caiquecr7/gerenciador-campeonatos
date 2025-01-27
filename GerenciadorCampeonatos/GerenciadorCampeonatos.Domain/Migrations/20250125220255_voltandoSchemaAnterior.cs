using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorCampeonatos.Domain.Migrations
{
    /// <inheritdoc />
    public partial class voltandoSchemaAnterior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "futebol");

            migrationBuilder.RenameTable(
                name: "Team",
                schema: "dbo",
                newName: "Team",
                newSchema: "futebol");

            migrationBuilder.RenameTable(
                name: "Player",
                schema: "dbo",
                newName: "Player",
                newSchema: "futebol");

            migrationBuilder.RenameTable(
                name: "MatchPlayer",
                schema: "dbo",
                newName: "MatchPlayer",
                newSchema: "futebol");

            migrationBuilder.RenameTable(
                name: "Match",
                schema: "dbo",
                newName: "Match",
                newSchema: "futebol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Team",
                schema: "futebol",
                newName: "Team",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Player",
                schema: "futebol",
                newName: "Player",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "MatchPlayer",
                schema: "futebol",
                newName: "MatchPlayer",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Match",
                schema: "futebol",
                newName: "Match",
                newSchema: "dbo");
        }
    }
}
