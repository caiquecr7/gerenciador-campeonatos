using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorCampeonatos.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlayerAge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth_Date",
                schema: "futebol",
                table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "futebol",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                schema: "futebol",
                table: "Player");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birth_Date",
                schema: "futebol",
                table: "Player",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
