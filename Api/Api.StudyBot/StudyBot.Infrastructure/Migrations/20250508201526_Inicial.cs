using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cronogramas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronogramas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiasSemanas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Dia = table.Column<string>(type: "text", nullable: false),
                    CronogramaId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiasSemanas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiasSemanas_Cronogramas_CronogramaId",
                        column: x => x.CronogramaId,
                        principalTable: "Cronogramas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Conteudo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DataEstudo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DiasSemanaId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conteudo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conteudo_DiasSemanas_DiasSemanaId",
                        column: x => x.DiasSemanaId,
                        principalTable: "DiasSemanas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conteudo_DiasSemanaId",
                table: "Conteudo",
                column: "DiasSemanaId");

            migrationBuilder.CreateIndex(
                name: "IX_DiasSemanas_CronogramaId",
                table: "DiasSemanas",
                column: "CronogramaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conteudo");

            migrationBuilder.DropTable(
                name: "DiasSemanas");

            migrationBuilder.DropTable(
                name: "Cronogramas");
        }
    }
}
