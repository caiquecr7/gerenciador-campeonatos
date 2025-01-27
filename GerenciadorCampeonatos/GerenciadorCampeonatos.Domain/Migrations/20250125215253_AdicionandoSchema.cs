using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorCampeonatos.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "futebol");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Team",
                newSchema: "futebol");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Player",
                newSchema: "futebol");

            migrationBuilder.RenameTable(
                name: "MatchPlayer",
                newName: "MatchPlayer",
                newSchema: "futebol");

            migrationBuilder.RenameTable(
                name: "Match",
                newName: "Match",
                newSchema: "futebol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Team",
                schema: "futebol",
                newName: "Team");

            migrationBuilder.RenameTable(
                name: "Player",
                schema: "futebol",
                newName: "Player");

            migrationBuilder.RenameTable(
                name: "MatchPlayer",
                schema: "futebol",
                newName: "MatchPlayer");

            migrationBuilder.RenameTable(
                name: "Match",
                schema: "futebol",
                newName: "Match");
        }
    }
}
