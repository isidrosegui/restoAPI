using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class AddestadoArqueo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "ArqueoCajas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EstadoArqueo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoArqueo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArqueoCajas_EstadoId",
                table: "ArqueoCajas",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArqueoCajas_EstadoArqueo_EstadoId",
                table: "ArqueoCajas",
                column: "EstadoId",
                principalTable: "EstadoArqueo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArqueoCajas_EstadoArqueo_EstadoId",
                table: "ArqueoCajas");

            migrationBuilder.DropTable(
                name: "EstadoArqueo");

            migrationBuilder.DropIndex(
                name: "IX_ArqueoCajas_EstadoId",
                table: "ArqueoCajas");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "ArqueoCajas");
        }
    }
}
