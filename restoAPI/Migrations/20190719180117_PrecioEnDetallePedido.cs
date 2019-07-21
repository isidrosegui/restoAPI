using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class PrecioEnDetallePedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrecioActualId",
                table: "DetallesPedido",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedido_PrecioActualId",
                table: "DetallesPedido",
                column: "PrecioActualId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedido_Precios_PrecioActualId",
                table: "DetallesPedido",
                column: "PrecioActualId",
                principalTable: "Precios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedido_Precios_PrecioActualId",
                table: "DetallesPedido");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPedido_PrecioActualId",
                table: "DetallesPedido");

            migrationBuilder.DropColumn(
                name: "PrecioActualId",
                table: "DetallesPedido");
        }
    }
}
