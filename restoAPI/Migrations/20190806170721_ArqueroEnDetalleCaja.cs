using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class ArqueroEnDetalleCaja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesArqueo_DetallesCaja_DetalleCajaId",
                table: "DetallesArqueo");

            migrationBuilder.DropIndex(
                name: "IX_DetallesArqueo_DetalleCajaId",
                table: "DetallesArqueo");

            migrationBuilder.AddColumn<int>(
                name: "ArqueoId",
                table: "DetallesCaja",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCaja_ArqueoId",
                table: "DetallesCaja",
                column: "ArqueoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCaja_ArqueoCajas_ArqueoId",
                table: "DetallesCaja",
                column: "ArqueoId",
                principalTable: "ArqueoCajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCaja_ArqueoCajas_ArqueoId",
                table: "DetallesCaja");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCaja_ArqueoId",
                table: "DetallesCaja");

            migrationBuilder.DropColumn(
                name: "ArqueoId",
                table: "DetallesCaja");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesArqueo_DetalleCajaId",
                table: "DetallesArqueo",
                column: "DetalleCajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesArqueo_DetallesCaja_DetalleCajaId",
                table: "DetallesArqueo",
                column: "DetalleCajaId",
                principalTable: "DetallesCaja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
