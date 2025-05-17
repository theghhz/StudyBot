using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AttDiasSemana : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dia",
                table: "DiasSemanas");

            migrationBuilder.DropColumn(
                name: "DataEstudo",
                table: "Conteudo");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "DiasSemanas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DiaEnum",
                table: "DiasSemanas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "DiasSemanas");

            migrationBuilder.DropColumn(
                name: "DiaEnum",
                table: "DiasSemanas");

            migrationBuilder.AddColumn<string>(
                name: "Dia",
                table: "DiasSemanas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEstudo",
                table: "Conteudo",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
