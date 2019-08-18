using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class ArreglosCajasYdetalles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cajas_DetallesCaja_DetalleAbiertoId",
                table: "Cajas");

            migrationBuilder.DropIndex(
                name: "IX_Cajas_DetalleAbiertoId",
                table: "Cajas");

            migrationBuilder.DropColumn(
                name: "DetalleAbiertoId",
                table: "Cajas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetalleAbiertoId",
                table: "Cajas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cajas_DetalleAbiertoId",
                table: "Cajas",
                column: "DetalleAbiertoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cajas_DetallesCaja_DetalleAbiertoId",
                table: "Cajas",
                column: "DetalleAbiertoId",
                principalTable: "DetallesCaja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
