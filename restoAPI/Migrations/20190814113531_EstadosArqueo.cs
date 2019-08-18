using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class EstadosArqueo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArqueoCajas_EstadoArqueo_EstadoId",
                table: "ArqueoCajas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadoArqueo",
                table: "EstadoArqueo");

            migrationBuilder.RenameTable(
                name: "EstadoArqueo",
                newName: "EstadosArqueo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadosArqueo",
                table: "EstadosArqueo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArqueoCajas_EstadosArqueo_EstadoId",
                table: "ArqueoCajas",
                column: "EstadoId",
                principalTable: "EstadosArqueo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArqueoCajas_EstadosArqueo_EstadoId",
                table: "ArqueoCajas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadosArqueo",
                table: "EstadosArqueo");

            migrationBuilder.RenameTable(
                name: "EstadosArqueo",
                newName: "EstadoArqueo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadoArqueo",
                table: "EstadoArqueo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArqueoCajas_EstadoArqueo_EstadoId",
                table: "ArqueoCajas",
                column: "EstadoId",
                principalTable: "EstadoArqueo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
