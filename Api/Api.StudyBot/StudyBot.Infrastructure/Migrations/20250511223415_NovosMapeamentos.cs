using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NovosMapeamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conteudo_DiasSemanas_DiasSemanaId",
                table: "Conteudo");

            migrationBuilder.DropForeignKey(
                name: "FK_DiasSemanas_Cronogramas_CronogramaId",
                table: "DiasSemanas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiasSemanas",
                table: "DiasSemanas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cronogramas",
                table: "Cronogramas");

            migrationBuilder.RenameTable(
                name: "DiasSemanas",
                newName: "DiasSemana");

            migrationBuilder.RenameTable(
                name: "Cronogramas",
                newName: "Cronograma");

            migrationBuilder.RenameIndex(
                name: "IX_DiasSemanas_CronogramaId",
                table: "DiasSemana",
                newName: "IX_DiasSemana_CronogramaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Cronograma",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiasSemana",
                table: "DiasSemana",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cronograma",
                table: "Cronograma",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conteudo_DiasSemana_DiasSemanaId",
                table: "Conteudo",
                column: "DiasSemanaId",
                principalTable: "DiasSemana",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiasSemana_Cronograma_CronogramaId",
                table: "DiasSemana",
                column: "CronogramaId",
                principalTable: "Cronograma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conteudo_DiasSemana_DiasSemanaId",
                table: "Conteudo");

            migrationBuilder.DropForeignKey(
                name: "FK_DiasSemana_Cronograma_CronogramaId",
                table: "DiasSemana");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiasSemana",
                table: "DiasSemana");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cronograma",
                table: "Cronograma");

            migrationBuilder.RenameTable(
                name: "DiasSemana",
                newName: "DiasSemanas");

            migrationBuilder.RenameTable(
                name: "Cronograma",
                newName: "Cronogramas");

            migrationBuilder.RenameIndex(
                name: "IX_DiasSemana_CronogramaId",
                table: "DiasSemanas",
                newName: "IX_DiasSemanas_CronogramaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Cronogramas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiasSemanas",
                table: "DiasSemanas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cronogramas",
                table: "Cronogramas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conteudo_DiasSemanas_DiasSemanaId",
                table: "Conteudo",
                column: "DiasSemanaId",
                principalTable: "DiasSemanas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiasSemanas_Cronogramas_CronogramaId",
                table: "DiasSemanas",
                column: "CronogramaId",
                principalTable: "Cronogramas",
                principalColumn: "Id");
        }
    }
}
