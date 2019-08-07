using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class ArqueoCaja2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArqueoCajaId",
                table: "DetallesArqueo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArqueoCajas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaArqueo = table.Column<DateTime>(nullable: false),
                    HoraArqueo = table.Column<TimeSpan>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    HoraBaja = table.Column<TimeSpan>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArqueoCajas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesArqueo_ArqueoCajaId",
                table: "DetallesArqueo",
                column: "ArqueoCajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesArqueo_ArqueoCajas_ArqueoCajaId",
                table: "DetallesArqueo",
                column: "ArqueoCajaId",
                principalTable: "ArqueoCajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesArqueo_ArqueoCajas_ArqueoCajaId",
                table: "DetallesArqueo");

            migrationBuilder.DropTable(
                name: "ArqueoCajas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesArqueo_ArqueoCajaId",
                table: "DetallesArqueo");

            migrationBuilder.DropColumn(
                name: "ArqueoCajaId",
                table: "DetallesArqueo");
        }
    }
}
