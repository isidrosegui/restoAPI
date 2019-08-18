using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class CajaIdInDetalleCaja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCaja_Cajas_CajaId",
                table: "DetallesCaja");

            migrationBuilder.AlterColumn<int>(
                name: "CajaId",
                table: "DetallesCaja",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCaja_Cajas_CajaId",
                table: "DetallesCaja",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCaja_Cajas_CajaId",
                table: "DetallesCaja");

            migrationBuilder.AlterColumn<int>(
                name: "CajaId",
                table: "DetallesCaja",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCaja_Cajas_CajaId",
                table: "DetallesCaja",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
